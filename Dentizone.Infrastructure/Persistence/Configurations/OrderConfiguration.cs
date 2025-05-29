using Dentizone.Domain.Entity;
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
            builder.Property(o => o.CommissionAmount).IsRequired();
            builder.Property(o => o.TotalAmount).IsRequired();
            builder.Property(o => o.UpdatedAt).IsRequired();

            builder.Property(o => o.CompletedAt).IsRequired(false);
            builder.Property(o => o.CreatedAt).IsRequired();

            builder.Property(o => o.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false);

            builder.HasOne(o => o.ShipInfo)
                .WithOne(si => si.Order)
                .HasForeignKey<Order>(o => o.ShipInfoId);


            builder.HasOne(o => o.Buyer)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.BuyerId);

            builder.HasMany(o => o.OrderPickups)
                .WithOne(op => op.Order)
                .HasForeignKey(op => op.OrderId);
        }
    }
}