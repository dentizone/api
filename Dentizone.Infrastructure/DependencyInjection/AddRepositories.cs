using Dentizone.Domain.Interfaces.Repositories;
using Dentizone.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Dentizone.Infrastructure.DependencyInjection
{
    internal static class AddRepositoriesServices
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserActivityRepository, UserActivityRepository>();
            services.AddScoped<IUniversityRepository, UniversityRepository>();
            services.AddScoped<ISubCategoryRepository, SubCategoryRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IPostAssetRepository, PostAssetRepository>();
            services.AddScoped<IAssetRepository, AssetRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderItemRepository, OrderItemRepository>();
            services.AddScoped<IOrderStatusRepository, OrderStatusRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<IShipInfoRepository, ShipInfoRepository>();
            services.AddScoped<IFavouriteRepository, FavouriteRepository>();
            services.AddScoped<IWalletRepository, WalletRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<ISaleTransactionRepository, SaleTransactionRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IWithdrawalRequestRepository, WithdrawalRequestRepository>();
            return services;
        }
    }
}