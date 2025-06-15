using Dentizone.Domain.Exceptions;
using Dentizone.Domain.Interfaces;
using Dentizone.Domain.Interfaces.Secret;
using Dentizone.Infrastructure.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using TokenValidationResult = Dentizone.Domain.Interfaces.TokenValidationResult;

namespace Dentizone.Application.Services.Authentication
{
    public class TokenService : ITokenService
    {
        private readonly IRedisService _redis;
        private readonly ISecretService _secretService;
        private readonly ILogger<TokenService> _logger;
        private readonly int _accessTokenDurationInMinutes = 60 * 2;

        private readonly int _refreshTokenDurationInMinutes = 60
                                                              * 6;

        private readonly string _accessSecretKey;
        private readonly string _refreshSecretKey;

        private TokenValidationParameters _accessTokenValidationParams;
        private TokenValidationParameters _refreshTokenValidationParams;

        private SigningCredentials _accessTokenCredentials;
        private SigningCredentials _refreshTokenCredentials;

        public TokenService(
            IRedisService redis,
            ISecretService secretService,
            ILogger<TokenService> logger
        )
        {
            _redis = redis ?? throw new ArgumentNullException(nameof(redis));
            _secretService = secretService ?? throw new ArgumentNullException(nameof(secretService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            try
            {
                _accessSecretKey = _secretService.GetSecret("JwtSecret");
                _refreshSecretKey = _secretService.GetSecret("JwtRefresh");

                ValidateSecretKeys();
                InitializeSigningCredentials();
                InitializeValidationParameters();

                _logger.LogInformation("TokenService initialized successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to initialize TokenService");
                throw new TokenConfigurationException("Failed to initialize TokenService", ex);
            }
        }

        private void ValidateSecretKeys()
        {
            if (string.IsNullOrEmpty(_accessSecretKey))
            {
                throw new TokenConfigurationException("JWT Access Secret key is not configured.");
            }

            if (string.IsNullOrEmpty(_refreshSecretKey))
            {
                throw new TokenConfigurationException("JWT Refresh Secret key is not configured.");
            }

            if (_accessSecretKey.Length < 32)
            {
                throw new TokenConfigurationException("JWT Access Secret key must be at least 32 characters long.");
            }

            if (_refreshSecretKey.Length < 32)
            {
                throw new TokenConfigurationException("JWT Refresh Secret key must be at least 32 characters long.");
            }
        }

        private void InitializeSigningCredentials()
        {
            var accessSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_accessSecretKey));
            _accessTokenCredentials = new SigningCredentials(accessSecurityKey, SecurityAlgorithms.HmacSha256Signature);

            var refreshSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_refreshSecretKey));
            _refreshTokenCredentials =
                new SigningCredentials(refreshSecurityKey, SecurityAlgorithms.HmacSha256Signature);
        }

        private void InitializeValidationParameters()
        {
            _accessTokenValidationParams = IdentityConfiguration.GetTokenValidationParameters(_secretService);

            _refreshTokenValidationParams = IdentityConfiguration.GetTokenValidationParameters(_refreshSecretKey);
        }

        private static IEnumerable<Claim> BuildAccessTokenClaims(string userId, string email, string role)
        {
            if (string.IsNullOrEmpty(userId))
                throw new ArgumentException("User ID cannot be null or empty", nameof(userId));
            if (string.IsNullOrEmpty(email))
                throw new ArgumentException("Email cannot be null or empty", nameof(email));
            if (string.IsNullOrEmpty(role)) throw new ArgumentException("Role cannot be null or empty", nameof(role));

            return new List<Claim>
                   {
                       new(ClaimTypes.NameIdentifier, userId),
                       new(ClaimTypes.Email, email),
                       new(ClaimTypes.Role, role),
                       new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                       new(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                           ClaimValueTypes.Integer64)
                   };
        }

        private static IEnumerable<Claim> BuildRefreshTokenClaims(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                throw new ArgumentException("User ID cannot be null or empty", nameof(userId));

            return new List<Claim>
                   {
                       new(ClaimTypes.NameIdentifier, userId),
                       new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                   };
        }

        public string GenerateAccessToken(string userId, string email, string role)
        {
            try
            {
                var tokenDescriptor = new SecurityTokenDescriptor
                                      {
                                          Subject = new ClaimsIdentity(BuildAccessTokenClaims(userId, email, role)),
                                          Expires =
                                              DateTime.UtcNow.AddMinutes(_accessTokenDurationInMinutes),
                                          SigningCredentials = _accessTokenCredentials,
                                          Issuer = _secretService.GetSecret("JwtIssuer"),
                                          Audience = _secretService.GetSecret("JwtAudience")
                                      };

                var handler = new JsonWebTokenHandler();
                var token = handler.CreateToken(tokenDescriptor);

                _logger.LogDebug("Access token generated successfully for user {UserId}", userId);
                return token;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to generate access token for user {UserId}", userId);
                throw;
            }
        }

        public string GenerateRefreshToken(string userId)
        {
            try
            {
                var tokenDescriptor = new SecurityTokenDescriptor
                                      {
                                          Subject = new ClaimsIdentity(BuildRefreshTokenClaims(userId)),
                                          Expires = DateTime.UtcNow.AddMinutes(
                                                                               _refreshTokenDurationInMinutes),
                                          SigningCredentials = _refreshTokenCredentials,
                                      };

                var handler = new JsonWebTokenHandler();
                var token = handler.CreateToken(tokenDescriptor);

                _logger.LogDebug("Refresh token generated successfully for user {UserId}", userId);
                return token;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to generate refresh token for user {UserId}", userId);
                throw;
            }
        }

        public async Task<TokenValidationResult> ValidateAccessTokenAsync(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return TokenValidationResult.Failure("Token is null or empty");
            }

            try
            {
                var tokenHandler = new JsonWebTokenHandler();

                if (!tokenHandler.CanReadToken(token))
                {
                    return TokenValidationResult.Failure("Token format is invalid");
                }

                // Check if token is blacklisted
                var tokenId = ExtractTokenId(token);
                if (!string.IsNullOrEmpty(tokenId) && await IsBlacklistedAsync(tokenId))
                {
                    return TokenValidationResult.Failure("Token has been revoked");
                }

                var validationResult = await tokenHandler.ValidateTokenAsync(token, _accessTokenValidationParams);

                if (validationResult.IsValid)
                {
                    return TokenValidationResult.Success(new ClaimsPrincipal(validationResult.ClaimsIdentity));
                }

                return TokenValidationResult.Failure("Token validation failed");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error validating access token");
                return TokenValidationResult.Failure($"Token validation error: {ex.Message}");
            }
        }

        public async Task<TokenValidationResult> ValidateRefreshTokenAsync(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return TokenValidationResult.Failure("Token is null or empty");
            }

            try
            {
                var tokenHandler = new JsonWebTokenHandler();

                if (!tokenHandler.CanReadToken(token))
                {
                    return TokenValidationResult.Failure("Token format is invalid");
                }

                // Check if token is blacklisted
                var tokenId = ExtractTokenId(token);
                if (!string.IsNullOrEmpty(tokenId) && await IsBlacklistedAsync(tokenId))
                {
                    return TokenValidationResult.Failure("Token has been revoked");
                }

                var validationResult = await tokenHandler.ValidateTokenAsync(token, _refreshTokenValidationParams);

                if (validationResult.IsValid)
                {
                    return TokenValidationResult.Success(new ClaimsPrincipal(validationResult.ClaimsIdentity));
                }

                return TokenValidationResult.Failure("Token validation failed");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error validating refresh token");
                return TokenValidationResult.Failure($"Token validation error: {ex.Message}");
            }
        }

        public async Task<bool> BlacklistAccessTokenAsync(string token)
        {
            return await BlacklistTokenInternalAsync(token, _accessTokenValidationParams, "access");
        }

        public async Task<bool> BlacklistRefreshTokenAsync(string token)
        {
            return await BlacklistTokenInternalAsync(token, _refreshTokenValidationParams, "refresh");
        }

        private async Task<bool> BlacklistTokenInternalAsync(string token, TokenValidationParameters validationParams,
                                                             string tokenType)
        {
            if (string.IsNullOrEmpty(token))
            {
                _logger.LogWarning("Attempted to blacklist null or empty {TokenType} token", tokenType);
                return false;
            }

            try
            {
                var tokenHandler = new JsonWebTokenHandler();

                if (!tokenHandler.CanReadToken(token))
                {
                    _logger.LogWarning("Cannot read {TokenType} token for blacklisting", tokenType);
                    return false;
                }

                var validationResult = await tokenHandler.ValidateTokenAsync(token, validationParams);

                if (!validationResult.IsValid)
                {
                    _logger.LogWarning("Invalid {TokenType} token cannot be blacklisted", tokenType);
                    return false;
                }

                var tokenPayload = tokenHandler.ReadJsonWebToken(token);
                var tokenId = tokenPayload.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti)?.Value;

                if (string.IsNullOrEmpty(tokenId))
                {
                    _logger.LogWarning("{TokenType} token missing JTI claim", tokenType);
                    return false;
                }

                var tokenExpiration = tokenPayload.ValidTo;
                var timeToExpiration = tokenExpiration - DateTime.UtcNow;

                if (timeToExpiration <= TimeSpan.Zero)
                {
                    _logger.LogDebug("{TokenType} token already expired, no need to blacklist", tokenType);
                    return true;
                }

                await _redis.SetValue(tokenId, token, timeToExpiration);
                _logger.LogDebug("{TokenType} token successfully blacklisted with ID {TokenId}", tokenType, tokenId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to blacklist {TokenType} token", tokenType);
                return false;
            }
        }

        public async Task<bool> IsBlacklistedAsync(string tokenId)
        {
            if (string.IsNullOrEmpty(tokenId))
            {
                return false;
            }

            try
            {
                var blacklistedToken = await _redis.GetValue(tokenId);
                return !string.IsNullOrEmpty(blacklistedToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking if token {TokenId} is blacklisted", tokenId);
                // Fail secure - if we can't check, assume it's not blacklisted
                // but log the error for investigation
                return false;
            }
        }

        public string? ExtractTokenId(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return null;
            }

            try
            {
                var tokenHandler = new JsonWebTokenHandler();

                if (!tokenHandler.CanReadToken(token))
                {
                    return null;
                }

                var tokenPayload = tokenHandler.ReadJsonWebToken(token);
                return tokenPayload.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti)?.Value;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to extract token ID from token");
                return null;
            }
        }

        public ClaimsPrincipal? ExtractClaims(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return null;
            }

            try
            {
                var tokenHandler = new JsonWebTokenHandler();

                if (!tokenHandler.CanReadToken(token))
                {
                    return null;
                }

                var tokenPayload = tokenHandler.ReadJsonWebToken(token);
                var identity = new ClaimsIdentity(tokenPayload.Claims, "jwt");
                return new ClaimsPrincipal(identity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to extract claims from token");
                return null;
            }
        }
    }
}