using Dentizone.Domain.Interfaces;
using Dentizone.Infrastructure.Cache;
using Microsoft.Extensions.DependencyInjection;

namespace Dentizone.Infrastructure.DependencyInjection
{
    public static class AddInfrastructureServices
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddRepositories();
            services.AddSqlServer();
            services.AddApiClients();
            services.AddAppIdentity();
            services.AddSecretManager();
            services.AddScoped<IRedisService, RedisService>();


            return services;
        }
    }
}