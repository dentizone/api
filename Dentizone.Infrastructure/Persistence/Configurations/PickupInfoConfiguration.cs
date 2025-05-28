using Dentizone.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dentizone.Infrastructure.Persistence.Configurations
{
    internal class PickupInfoConfiguration : IEntityTypeConfiguration<PickupInfo>
    {
        public void Configure(EntityTypeBuilder<PickupInfo> builder)
        {
            builder.Property(i => i.Id).IsRequired();
            builder.Property(i => i.street).IsRequired();
            builder.Property(i => i.city).IsRequired();


            builder.HasKey(i => i.Id);

            builder.HasOne(o => o.Seller)
                .WithMany(o => o.PickupInfos)
                .HasForeignKey(o => o.SellerId);

            builder.HasOne(p => p.Post)
                .WithOne(pi => pi.pickupinfo)
                .HasForeignKey<PickupInfo>(pi => pi.PostId);






        }
    }
}
