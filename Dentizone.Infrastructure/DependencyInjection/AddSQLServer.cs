using Dentizone.Domain.Interfaces.Secret;
using Dentizone.Infrastructure.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Dentizone.Infrastructure.DependencyInjection
{
    internal static class InjectSqlServer
    {
        public static IServiceCollection AddSqlServer(this IServiceCollection services)
        {
            services.AddScoped<BaseEntityInterceptor>();
            services.AddDbContext<AppDbContext>((serviceProvider, options) =>
            {
                var interceptor = serviceProvider.GetRequiredService<BaseEntityInterceptor>();
                var configuration = serviceProvider.GetRequiredService<ISecretService>();
                options.UseSqlServer(configuration.GetSecret("DentizoneDb"))
                       .AddInterceptors(interceptor);
            });

            return services;
        }
    }
}