using Dentizone.Domain.Interfaces;
using Dentizone.Infrastructure.Cache;
using Dentizone.Infrastructure.Mongo;
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
            services.AddBackgroundService();

            // Register MongoDbService as a singleton
            services.AddSingleton<MongoDbService>();

            return services;
        }
    }
}