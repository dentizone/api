using Dentizone.Domain.Interfaces.Secret;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Dentizone.Infrastructure.Identity
{
    public static class IdentityConfiguration
    {
        public static PasswordOptions PasswordRestrictions { get; } = new()
        {
            RequireDigit = true,
            RequiredLength = 6,
            RequireLowercase = true,
            RequireNonAlphanumeric = true,
            RequireUppercase = true,
        };

        public static SignInOptions SignInOptions { get; } = new()
        {
            RequireConfirmedAccount = false,
            RequireConfirmedEmail = true,
            RequireConfirmedPhoneNumber = false,
        };

        public static LockoutOptions LockoutOptions { get; } = new()
        {
            AllowedForNewUsers = true,
            DefaultLockoutTimeSpan =
                                                                       TimeSpan.FromMinutes(15),
            MaxFailedAccessAttempts = 3,
        };


        public static TokenValidationParameters GetTokenValidationParameters(ISecretService secretService)
        {
            return new TokenValidationParameters
            {
                ClockSkew = TimeSpan.Zero,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = secretService.GetSecret("JwtIssuer"),
                ValidAudience = secretService.GetSecret("JwtAudience"),
                IssuerSigningKey = new SymmetricSecurityKey(
                                                                   Encoding.UTF8
                                                                           .GetBytes(secretService
                                                                                    .GetSecret("JwtSecret"))
                                                                  ),
            };
        }

        public static TokenValidationParameters GetTokenValidationParameters(string secret)
        {
            return new TokenValidationParameters
            {
                ClockSkew = TimeSpan.Zero,
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                                                                   Encoding.UTF8
                                                                           .GetBytes(secret)
                                                                  ),
            };
        }
    }
}