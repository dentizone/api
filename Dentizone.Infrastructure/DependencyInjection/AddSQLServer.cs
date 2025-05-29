using Dentizone.Infrastructure.Persistence.Interceptors;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Dentizone.Infrastructure.DependencyInjection
{
    public static class InjectSQLServer
    {
        public static IServiceCollection AddSQLServer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<BaseEntityInterceptor>();
            services.AddDbContext<AppDbContext>((serviceProvider, options) =>
            {
                var interceptor = serviceProvider.GetRequiredService<BaseEntityInterceptor>();
                options.UseSqlServer(configuration.GetConnectionString("DentizoneDb"))
                    .AddInterceptors(interceptor);
            });

            return services;
        }
    }
}