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
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.id);

            builder.Property(o => o.buyer_id).IsRequired();
            builder.Property(o => o.status).IsRequired();
            builder.Property(o => o.placed_at).IsRequired();
            builder.Property(o => o.commission_amount).IsRequired();
            builder.Property(o => o.total_amount).IsRequired();
            builder.Property(o => o.updated_at).IsRequired();

            builder.HasOne(o => o.user)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.buyer_id);
        }
    }
}
