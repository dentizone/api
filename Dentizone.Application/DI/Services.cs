using Dentizone.Application.Interfaces;
using Dentizone.Application.Jobs;
using Dentizone.Application.Services;
using Dentizone.Application.Services.Authentication;
using Dentizone.Application.Services.Payment;
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
            services.AddScoped<IAnalyticsService, AnalyticsService>();
            services.AddScoped<IFavoritesService, FavoriteService>();
            services.AddScoped<IVerificationService, VerificationService>();
            services.AddScoped<IWalletService, WalletService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<IWithdrawalService, WithdrawalService>();
            services.AddScoped<IQaService, QaService>();


            services.AddScoped<IMonitorJob, MonitorJob>();

            services.AddScoped<IShippingService, ShippingService>();



            return services;
        }
    }
}