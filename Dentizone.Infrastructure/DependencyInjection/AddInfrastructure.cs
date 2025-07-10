using Dentizone.Domain.Interfaces;
using Dentizone.Infrastructure.Cache;
using Dentizone.Infrastructure.Identity;
using Dentizone.Infrastructure.Mongo;
using Microsoft.AspNetCore.Identity;
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
            services.AddBackgroundService();

            // Register MongoDbService as a singleton
            services.AddSingleton<IMongoDbService, MongoDbService>();
            services.AddSingleton<IRedisService, RedisService>();
            services.AddScoped<IResetTokenStore, RedisResetTokenStore>();

            services.AddTransient<UuidPasswordResetTokenProvider<ApplicationUser>>(sp =>
            {
                var store = sp.GetRequiredService<IResetTokenStore>();
                return new UuidPasswordResetTokenProvider<ApplicationUser>(store);
            });

            services.PostConfigure<IdentityOptions>(options =>
            {
                options.Tokens.ProviderMap["uuid"] = new TokenProviderDescriptor(typeof(UuidPasswordResetTokenProvider<ApplicationUser>));
            });

            return services;
        }
    }
}