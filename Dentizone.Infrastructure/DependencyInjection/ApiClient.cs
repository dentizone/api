using Dentizone.Infrastructure.ApiClient;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace Dentizone.Infrastructure.DependencyInjection
{
    internal static class ApiClient
    {
        public static IServiceCollection AddApiClients(this IServiceCollection services)
        {
            services.AddRefitClient<ITruboSMTP>()
                    .ConfigureHttpClient(c => { c.BaseAddress = new Uri("https://api.turbo-smtp.com/api/v2"); });
            return services;
        }
    }
}