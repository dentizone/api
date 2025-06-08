using Dentizone.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Dentizone.Infrastructure.Filters
{
    public class BlacklistAuthenticationFilter : IAsyncAuthorizationFilter
    {
        private readonly ITokenService _tokenService;
        private readonly ILogger<BlacklistAuthenticationFilter> _logger;

        public BlacklistAuthenticationFilter(ITokenService tokenService, ILogger<BlacklistAuthenticationFilter> logger)
        {
            _tokenService = tokenService;
            _logger = logger;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            // Skip if endpoint allows anonymous access
            if (context.ActionDescriptor.EndpointMetadata.Any(x => x is AllowAnonymousAttribute))
            {
                return;
            }

            // Skip if no authorization is required
            if (!context.ActionDescriptor.EndpointMetadata.Any(x => x is AuthorizeAttribute))
            {
                return;
            }

            try
            {
                var token = ExtractTokenFromRequest(context.HttpContext.Request);

                if (string.IsNullOrEmpty(token))
                {
                    // Let the normal authentication handle missing tokens ;)
                    return;
                }

                var tokenId = _tokenService.ExtractTokenId(token);

                if (string.IsNullOrEmpty(tokenId))
                {
                    _logger.LogWarning("Token missing JTI claim");
                    context.Result = CreateUnauthorizedResult("Invalid token format");
                    return;
                }

                var isBlacklisted = await _tokenService.IsBlacklistedAsync(tokenId);

                if (isBlacklisted)
                {
                    _logger.LogWarning("Blacklisted token attempted access. Token ID: {TokenId}", tokenId);
                    context.Result = CreateUnauthorizedResult("Token has been revoked");
                    return;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking token blacklist status");
                context.Result = CreateUnauthorizedResult("Authentication error");
            }
        }

        private static string? ExtractTokenFromRequest(HttpRequest request)
        {
            var authHeader = request.Headers["Authorization"].FirstOrDefault();

            if (authHeader != null && authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                return authHeader.Substring("Bearer ".Length).Trim();
            }

            return null;
        }

        private static JsonResult CreateUnauthorizedResult(string message)
        {
            return new JsonResult(new { message })
            {
                StatusCode = 401
            };
        }
    }
}