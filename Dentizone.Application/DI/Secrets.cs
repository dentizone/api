using Dentizone.Application.Interfaces.Secret;
using Dentizone.Application.Services;
using dotenv.net;
using Infisical.Sdk;
using Microsoft.Extensions.DependencyInjection;

namespace Dentizone.Application.DI
{
    public static class Secrets
    {
        /// <summary>
        /// Configures and registers secret management services, including Infisical client and secret service, using credentials from environment variables.
        /// </summary>
        /// <param name="services">The service collection to which secret management services will be added.</param>
        /// <returns>The updated service collection with secret management services registered.</returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if required Infisical credentials are not found in environment variables.
        /// </exception>
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

            services.AddSingleton(infisicalClient);

            services.AddScoped<ISecretService, SecretService>();

            return services;
        }
    }
}