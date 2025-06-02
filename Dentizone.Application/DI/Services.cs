using Dentizone.Application.Interfaces.Mail;
using Dentizone.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Dentizone.Application.DI
{
    public static class Services
    {
        /// <summary>
        /// Registers application-level services and API clients into the dependency injection container.
        /// </summary>
        /// <param name="services">The service collection to configure.</param>
        /// <returns>The updated service collection with application services registered.</returns>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddApiClients();

            services.AddScoped<IMailService, MailService>();
            return services;
        }
    }
}