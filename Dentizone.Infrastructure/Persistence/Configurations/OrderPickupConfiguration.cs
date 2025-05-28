using Dentizone.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dentizone.Infrastructure.Persistence.Configurations
{
    internal class OrderPickupConfiguration : IEntityTypeConfiguration<OrderPickup>
    {
        public void Configure(EntityTypeBuilder<OrderPickup> builder)
        {
            builder.HasKey(op => op.Id);

            builder.Property(op => op.OrderId).IsRequired();
            builder.Property(op => op.PickupId).IsRequired();
            builder.Property(op => op.CreatedAt).IsRequired();
            builder.Property(op => op.UpdatedAt).IsRequired();


            builder.HasOne(op => op.Order)
                .WithMany(o => o.OrderPickups)
                .HasForeignKey(op => op.OrderId);





            builder.HasOne(op => op.Pickup)
                .WithMany(p => p.OrderPickups)
                .HasForeignKey(op => op.PickupId);

        }
    }
}
