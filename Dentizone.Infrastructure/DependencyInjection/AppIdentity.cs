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


            return services;
        }
    }
}