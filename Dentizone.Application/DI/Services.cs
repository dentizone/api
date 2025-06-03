using Dentizone.Application.Interfaces.Cloudinary;
using Dentizone.Application.Interfaces.Mail;
using Dentizone.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Dentizone.Application.DI
{
    public static class Services
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddApiClients();

            services.AddScoped<IMailService, MailService>();
            services.AddScoped<ICloudinaryService, CloudinaryService>();
            return services;
        }
    }
}