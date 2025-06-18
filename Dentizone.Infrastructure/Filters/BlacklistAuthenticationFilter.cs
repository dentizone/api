using Dentizone.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Dentizone.Infrastructure.Filters
{
    public class BlacklistAuthenticationFilter(
        ITokenService tokenService,
        ILogger<BlacklistAuthenticationFilter> logger)
        : IAsyncAuthorizationFilter
    {
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

                var tokenId = tokenService.ExtractTokenId(token);

                if (string.IsNullOrEmpty(tokenId))
                {
                    logger.LogWarning("Token missing JTI claim");
                    context.Result = CreateUnauthorizedResult("Invalid token format");
                    return;
                }

                var isBlacklisted = await tokenService.IsBlacklistedAsync(tokenId);

                if (isBlacklisted)
                {
                    logger.LogWarning("Blacklisted token attempted access. Token ID: {TokenId}", tokenId);
                    context.Result = CreateUnauthorizedResult("Token has been revoked");
                    return;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error checking token blacklist status");
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