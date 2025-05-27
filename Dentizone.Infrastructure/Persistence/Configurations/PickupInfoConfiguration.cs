using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dentizone.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dentizone.Infrastructure.Persistence.Configurations
{
    internal class PickupInfoConfiguration : IEntityTypeConfiguration<PickupInfo>
    {
        public void Configure(EntityTypeBuilder<PickupInfo> builder)
        {
            builder.Property(i => i.id).IsRequired();
            builder.Property(i => i.street).IsRequired();
            builder.Property(i => i.city).IsRequired();
            builder.Property(i => i.post_id).IsRequired();
            builder.Property(i => i.created_at).IsRequired();

            builder.HasKey(i => i.id);

            builder.HasOne(i => i.Post)
                   .WithOne(p => p.pickupinfo)
                   .HasForeignKey<PickupInfo>(i => i.post_id);

            builder.HasOne(p => p.Order)
                    .WithMany(o => o.PickupInfos)
                    .HasForeignKey(p => p.order_id);


        }
    }
}
