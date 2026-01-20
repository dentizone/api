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
            services.AddRefitClient<IAiLayer>()
                    .ConfigureHttpClient(c => { c.BaseAddress = new Uri("https://ai.dentizone.store"); });
            return services;
        }
    }
}