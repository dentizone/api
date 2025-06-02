using Dentizone.Application.Interfaces.ApiClient;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace Dentizone.Application.DI
{
    public static class ApiClient
    {
        /// <summary>
        /// Registers the ITruboSMTP Refit HTTP client with a predefined base address for dependency injection.
        /// </summary>
        /// <returns>The updated IServiceCollection instance.</returns>
        public static IServiceCollection AddApiClients(this IServiceCollection services)
        {
            services.AddRefitClient<ITruboSMTP>()
                    .ConfigureHttpClient(c => { c.BaseAddress = new Uri("https://api.turbo-smtp.com/api/v2"); });
            return services;
        }
    }
}