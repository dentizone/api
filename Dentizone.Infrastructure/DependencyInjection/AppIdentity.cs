using Dentizone.Domain.Enums;
using Dentizone.Domain.Interfaces.Secret;
using Dentizone.Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Dentizone.Infrastructure.DependencyInjection
{
    internal static class AppIdentity
    {
        public static IServiceCollection AddAppIdentity(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
                {
                    options.User.RequireUniqueEmail = true;
                    options.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
                    options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultProvider;
                    options.Password = IdentityConfiguration.PasswordRestrictions;
                    options.SignIn = IdentityConfiguration.SignInOptions;
                    options.Lockout = IdentityConfiguration.LockoutOptions;
                })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer();

            services.PostConfigure<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                using var scope = services.BuildServiceProvider().CreateScope();
                var secretService = scope.ServiceProvider.GetRequiredService<ISecretService>();
                options.TokenValidationParameters = IdentityConfiguration.GetTokenValidationParameters(secretService);
            });
            services.AddAuthorization(options =>
            {
                // Policy for actions that require a fully verified (KYC) user
                options.AddPolicy("IsVerified", policy =>
                    policy.RequireRole(UserRoles.VERIFIED.ToString()));

                // Policy for actions that require at least an email-verified user
                options.AddPolicy("IsPartilyVerified", policy =>
                    policy.RequireRole(
                        UserRoles.PARTILY_VERIFIED.ToString(),
                        UserRoles.VERIFIED.ToString() // A verified user is also partially verified
                    ));

                // Policy for admin-only actions
                options.AddPolicy("IsAdmin", policy =>
                    policy.RequireRole(UserRoles.ADMIN.ToString()));
            });


            return services;
        }
    }
}