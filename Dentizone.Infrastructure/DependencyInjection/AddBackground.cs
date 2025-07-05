using Dentizone.Domain.Interfaces;
using Dentizone.Domain.Interfaces.Secret;
using Dentizone.Infrastructure.Hangfire;
using Hangfire;
using Microsoft.Extensions.DependencyInjection;

namespace Dentizone.Infrastructure.DependencyInjection
{
    public static class AddBackground
    {
        public static IServiceCollection AddBackgroundService(this IServiceCollection services)
        {
            var scope = services.BuildServiceProvider();
            var SecretService = scope.GetRequiredService<ISecretService>();
            services.AddHangfire(config =>
                                     config.UseSqlServerStorage(SecretService.GetSecret("HangfireDB")));
            services.AddHangfireServer();
            services.AddScoped<IBackgroundJobService, HangfireBackgroundJobService>();

            return services;
        }
    }
}