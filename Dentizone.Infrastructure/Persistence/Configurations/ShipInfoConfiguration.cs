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
    internal class ShipInfoConfiguration : IEntityTypeConfiguration<ShipInfo>
    {
        public void Configure(EntityTypeBuilder<ShipInfo> builder)
        {
            builder.HasKey(i => i.id);

            builder.Property(i => i.id).IsRequired();
            builder.Property(i => i.street).IsRequired();
            builder.Property(i => i.city).IsRequired();
            builder.Property(i => i.order_id).IsRequired();
            builder.Property(i => i.user_id).IsRequired();
            builder.Property(i => i.created_at).IsRequired();

            builder.HasOne(s => s.user)
                .WithMany(u => u.ShippingAddresses) 
                .HasForeignKey(s => s.user_id);

            builder.HasOne(s => s.order)
    .WithOne(o => o.ShipInfo)
    .HasForeignKey<ShipInfo>(s => s.order_id);





        }
    }
}
