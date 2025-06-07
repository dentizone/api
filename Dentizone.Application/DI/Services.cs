using Dentizone.Application.Interfaces;
using Dentizone.Application.Interfaces.Cloudinary;
using Dentizone.Application.Interfaces.User;
using Dentizone.Application.Services;
using Dentizone.Application.Services.Authentication;
using Dentizone.Domain.Interfaces.Mail;
using Microsoft.Extensions.DependencyInjection;

namespace Dentizone.Application.DI
{
    public static class Services
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMailService, MailService>();
            services.AddScoped<ICloudinaryService, CloudinaryService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserActivityService, UserActivityService>();


            services.AddScoped<VerificationService, VerificationService>();

            return services;
        }
    }
}