using Dentizone.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dentizone.Infrastructure.Persistence.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(u => u.FullName)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(u => u.AcademicYear)
                .IsRequired();

            builder.Property(u => u.UniversityId)
                .IsRequired();
            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(u => u.NationalId);

            builder.Property(u => u.KycStatus)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(u => u.Status)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(u => u.CreatedAt)
                .HasDefaultValueSql(SqlCommon.Date)
                .ValueGeneratedOnAdd();


            builder.Property(u => u.UpdatedAt)
                .IsRequired();

            builder.Property(u => u.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false);


            // Relationships
            // One-to-One: User to Wallet
            builder.HasOne(u => u.Wallet)
                .WithOne(w => w.User)
                .HasForeignKey<Wallet>(w => w.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            // One-to-Many: User to UserAssets
            builder.HasMany(u => u.UserAssets)
                .WithOne(ua => ua.User)
                .HasForeignKey(ua => ua.UserId)
                .OnDelete(DeleteBehavior.NoAction);


            // One-to-One: User to University
            builder.HasOne(u => u.University)
                .WithMany(un => un.Users)
                .HasForeignKey(u => u.UniversityId)
                .OnDelete(DeleteBehavior.NoAction);


            // One-to-Many: User to UserActivities
            builder.HasMany(u => u.UserActivities)
                .WithOne(ua => ua.User)
                .HasForeignKey(ua => ua.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            // One-to-Many: User to Posts
            builder.HasMany(u => u.Posts)
                .WithOne(p => p.Seller)
                .HasForeignKey(p => p.SellerId)
                .OnDelete(DeleteBehavior.NoAction);

            // One-to-Many: User to Questions
            builder.HasMany(u => u.Questions)
                .WithOne(q => q.User)
                .HasForeignKey(q => q.AskerId)
                .OnDelete(DeleteBehavior.NoAction);


            // One-to-Many: User to ShippingAddresses
            builder.HasMany(u => u.ShippingAddresses)
                .WithOne(sa => sa.User)
                .HasForeignKey(sa => sa.UserId)
                .OnDelete(DeleteBehavior.NoAction);


            // One-to-Many: User to Orders
            builder.HasMany(u => u.Orders)
                .WithOne(o => o.Buyer)
                .HasForeignKey(o => o.BuyerId)
                .OnDelete(DeleteBehavior.NoAction);

            // One-to-Many: User to ReviewsGiven
            builder.HasMany(u => u.Reviews)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.NoAction);


            // One-to-Many: User to CartItems
            builder.HasMany(u => u.Carts)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.NoAction);


            builder.HasMany(u => u.Favourites)
                .WithOne(f => f.User)
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(u => u.Payments)
                .WithOne(f => f.Buyer)
                .HasForeignKey(f => f.BuyerId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}