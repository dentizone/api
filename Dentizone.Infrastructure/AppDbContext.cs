using Dentizone.Domain.Entity;
using Dentizone.Infrastructure.Identity;
using Dentizone.Infrastructure.Persistence.Interceptors;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Dentizone.Infrastructure
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Answer> Answers { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Asset> Assets { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Favourite> Favourites { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<OrderPickup> OrderPickups { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostAsset> PostAssets { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<ReviewUx> ReviewUxes { get; set; }
        public DbSet<ShipInfo> ShipInfos { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<University> Universities { get; set; }
        public DbSet<UserActivity> UserActivities { get; set; }
        public DbSet<UserAsset> UserAssets { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<WalletActivity> WalletActivities { get; set; }
        public DbSet<WithdrawalRequest> WithdrawalRequests { get; set; }

        public DbSet<ShipmentActivity> ShipmentActivities { get; set; }
        public DbSet<SalesTransaction> SalesTransactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(new BaseEntityInterceptor());
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}