using Dentizone.Application.Interfaces;
using Dentizone.Application.Interfaces.Assets;
using Dentizone.Application.Interfaces.Cart;
using Dentizone.Application.Interfaces.Catalog;
using Dentizone.Application.Interfaces.Cloudinary;
using Dentizone.Application.Interfaces.Order;
using Dentizone.Application.Interfaces.Post;
using Dentizone.Application.Interfaces.User;
using Dentizone.Application.Services;
using Dentizone.Application.Services.Authentication;
using Dentizone.Domain.Interfaces;
using Dentizone.Domain.Interfaces.Mail;
using Microsoft.Extensions.DependencyInjection;

namespace Dentizone.Application.DI
{
    public static class Services
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IPostService, PostService>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMailService, MailService>();
            services.AddScoped<IUniversityService, UniversityService>();
            services.AddScoped<ICloudinaryService, CloudinaryService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserActivityService, UserActivityService>();
            services.AddScoped<ICatalogService, CatalogService>();
            services.AddScoped<IUploadService, UploadService>();
            services.AddScoped<IAssetService, AssetService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ICartService, CartService>();


            services.AddScoped<IVerificationService, VerificationService>();

            return services;
        }
    }
}