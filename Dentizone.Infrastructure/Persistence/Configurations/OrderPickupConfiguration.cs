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
    internal class OrderPickupConfiguration : IEntityTypeConfiguration<OrderPickup>
    {
        public void Configure(EntityTypeBuilder<OrderPickup> builder)
        {
            builder.HasKey(op => op.id);

            builder.Property(op => op.order_id).IsRequired();
            builder.Property(op => op.pickup_id).IsRequired();

            builder.HasOne(op => op.order)
                .WithMany(o => o.OrderPickups)
                .HasForeignKey(op => op.order_id);


            builder.HasOne(op => op.pickup)
                .WithMany(p => p.OrderPickups)
                .HasForeignKey(op => op.pickup_id);
                
        }
    }
}
