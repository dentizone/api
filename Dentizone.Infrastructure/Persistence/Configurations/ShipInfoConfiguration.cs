using Dentizone.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dentizone.Infrastructure.Persistence.Configurations
{
    internal class ShipInfoConfiguration : IEntityTypeConfiguration<ShipInfo>
    {
        public void Configure(EntityTypeBuilder<ShipInfo> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.Id).IsRequired();
            builder.Property(i => i.Street).IsRequired();
            builder.Property(i => i.City).IsRequired();
            builder.Property(i => i.OrderId).IsRequired();
            builder.Property(i => i.UserId).IsRequired();
            builder.Property(i => i.CreatedAt).IsRequired();

            builder.HasOne(s => s.User)
                .WithMany(u => u.ShippingAddresses)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(s => s.Order)
                .WithOne(o => o.ShipInfo)
                .HasForeignKey<ShipInfo>(s => s.OrderId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}