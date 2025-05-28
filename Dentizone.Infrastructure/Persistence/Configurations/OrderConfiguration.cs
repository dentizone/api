using Dentizone.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dentizone.Infrastructure.Persistence.Configurations
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(o => o.BuyerId).IsRequired();
            builder.Property(o => o.Status).IsRequired();
            builder.Property(o => o.PlacedAt).IsRequired();
            builder.Property(o => o.CommissionAmount).IsRequired();
            builder.Property(o => o.TotalAmount).IsRequired();
            builder.Property(o => o.UpdatedAt).IsRequired();

            builder.Property(o => o.CompletedAt).IsRequired(false);
            builder.Property(o => o.CreatedAt).IsRequired();


            builder.HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.BuyerId);

            builder.HasMany(o => o.OrderPickups)
                   .WithOne(op => op.Order)
                   .HasForeignKey(op => op.OrderId);
        }
    }
}
