using Dentizone.Domain.Interfaces.Secret;
using Dentizone.Infrastructure.Secret;
using dotenv.net;
using Infisical.Sdk;
using Microsoft.Extensions.DependencyInjection;

namespace Dentizone.Application.DI
{
    public static class Secrets
    {
        public static IServiceCollection AddSecretManager(this IServiceCollection services)
        {
            DotEnv.Load();
            var clientId = Environment.GetEnvironmentVariable("ClientId");
            var clientSecret = Environment.GetEnvironmentVariable("ClientSecret");

            if (string.IsNullOrEmpty(clientId) || string.IsNullOrEmpty(clientSecret))
            {
                throw new InvalidOperationException("Infisical credentials not configured in environment variables.");
            }

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

            var infisicalClient = new InfisicalClient(settings);

            services.AddScoped(_ => infisicalClient);

            services.AddScoped<ISecretService, SecretService>();

            return services;
        }
    }
}