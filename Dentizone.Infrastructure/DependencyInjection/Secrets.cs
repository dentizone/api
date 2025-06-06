using Dentizone.Domain.Interfaces.Secret;
using Dentizone.Infrastructure.Secret;
using dotenv.net;
using Infisical.Sdk;
using Microsoft.Extensions.DependencyInjection;

namespace Dentizone.Infrastructure.DependencyInjection
{
    internal static class Secrets
    {
        public static IServiceCollection AddSecretManager(this IServiceCollection services)
        {
            DotEnv.Load();

            services.AddScoped<InfisicalClient>(_ =>
            {
                var clientId = Environment.GetEnvironmentVariable("ClientId");
                var clientSecret = Environment.GetEnvironmentVariable("ClientSecret");

                if (string.IsNullOrEmpty(clientId) || string.IsNullOrEmpty(clientSecret))
                    throw new
                        InvalidOperationException("Infisical credentials not configured in environment variables.");

                var settings = new ClientSettings
                {
                    Auth = new AuthenticationOptions
                    {
                        UniversalAuth = new UniversalAuthMethod
                        {
                            ClientId = clientId,
                            ClientSecret = clientSecret
                        }
                    }
                };

                return new InfisicalClient(settings);
            });

            services.AddScoped<ISecretService, SecretService>();

            return services;
        }
    }
}