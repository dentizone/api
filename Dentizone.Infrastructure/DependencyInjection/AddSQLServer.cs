using Dentizone.Domain.Interfaces.Secret;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Dentizone.Infrastructure.DependencyInjection
{
    internal static class InjectSqlServer
    {
        public static IServiceCollection AddSqlServer(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>((serviceProvider, options) =>
            {
                var configuration = serviceProvider.GetRequiredService<ISecretService>();
                options.UseSqlServer(configuration.GetSecret("DentizoneDb"));
            });

            return services;
        }
    }
}