using Dentizone.Domain.Interfaces;
using Dentizone.Domain.Interfaces.Secret;
using Dentizone.Infrastructure.Cache;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace Dentizone.Infrastructure.Identity
{
    public class TokenService : ITokenService
    {
        private readonly IRedisService _redis;
        private readonly ISecretService _secretService;
        private const int DurationInMinutes = 5;
        private readonly SigningCredentials _credentials;

        public TokenService(IRedisService redis, ISecretService secretService)
        {
            _redis = redis;
            _secretService = secretService;
            var secretKey = _secretService.GetSecret("JwtSecret");
            if (string.IsNullOrEmpty(secretKey))
            {
                throw new TokenConfigurationException("JWT Secret key is not configured.");
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            _credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
        }

        private IEnumerable<Claim> BuildClaims(string id, string email, string role)
        {
            return new List<Claim>
                   {
                       new(ClaimTypes.NameIdentifier, id),
                       new(ClaimTypes.Email, email),
                       new(JwtRegisteredClaimNames.Jti,
                           Guid.NewGuid()
                               .ToString()), // This is a Claim that is used to identify this token exactly. for blacklisting.
                       new(ClaimTypes.Role, role)
                   };
        }

        public string GenerateToken(string id, string email, string role)
        {
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(BuildClaims(id, email, role)),
                Expires = DateTime.UtcNow.AddMinutes(DurationInMinutes),
                SigningCredentials = _credentials,
                Issuer = _secretService.GetSecret("JwtIssuer"),
                Audience = _secretService.GetSecret("JwtAudience"),
            };
            var handler = new JsonWebTokenHandler();
            var token = handler.CreateToken(tokenDescriptor);
            return token;
        }

        public async Task BlacklistToken(string token)
        {
            var tokenHandler = new JsonWebTokenHandler();


            // Check first if the token is valid,
            // i don't want spammy requests to my redis server. -_-

            var isValid = tokenHandler.CanReadToken(token);
            if (!isValid)
            {
                return;
            }

            // If the token is valid,
            // then we need to check if it's already expired.
            var validationResult = await tokenHandler.ValidateTokenAsync(token,
                                                                         IdentityConfiguration
                                                                             .GetTokenValidationParameters(_secretService)
                                                                        );

            if (validationResult.IsValid)
            {
                var tokenPayload = tokenHandler.ReadJsonWebToken(token);
                var tokenId = tokenPayload.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti)?.Value;
                var tokenExpiresIn = tokenPayload.ValidTo;

                var expiresAtInTimeSpan = tokenExpiresIn - DateTime.UtcNow;
                if (tokenId != null)
                {
                    _redis.SetValue(tokenId, token, expiresAtInTimeSpan);
                }
            }
        }

        public bool IsBlacklisted(string tokenId)
        {
            var blacklistedToken = _redis.GetValue(tokenId);
            return blacklistedToken != null;
        }
    }


    public class TokenConfigurationException(string message) : Exception(message);
}