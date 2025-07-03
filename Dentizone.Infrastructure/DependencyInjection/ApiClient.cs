using Dentizone.Infrastructure.ApiClient;
using Dentizone.Infrastructure.ApiClient.KYC;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace Dentizone.Infrastructure.DependencyInjection
{
    internal static class ApiClient
    {
        public static IServiceCollection AddApiClients(this IServiceCollection services)
        {
            services.AddRefitClient<IDiditApi>()
                    .ConfigureHttpClient(c => { c.BaseAddress = new Uri("https://verification.didit.me"); });
            services.AddRefitClient<ITruboSmtp>()
                    .ConfigureHttpClient(c => { c.BaseAddress = new Uri("https://api.turbo-smtp.com/api/v2"); });
            services.AddRefitClient<IAILayer>()
                    .ConfigureHttpClient(c => { c.BaseAddress = new Uri("http://localhost:8000"); });
            return services;
        }
    }
}