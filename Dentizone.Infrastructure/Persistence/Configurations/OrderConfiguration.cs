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

            builder.Property(o => o.buyer_id).IsRequired();
            builder.Property(o => o.status).IsRequired();
            builder.Property(o => o.placed_at).IsRequired();
            builder.Property(o => o.commission_amount).IsRequired();
            builder.Property(o => o.total_amount).IsRequired();
            builder.Property(o => o.updated_at).IsRequired();

            builder.Property(o => o.completed_at).IsRequired(false);
            builder.Property(o => o.CreatedAt).IsRequired();


            builder.HasOne(o => o.user)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.buyer_id);

            builder.HasMany(o => o.OrderPickups)
                   .WithOne(op => op.order)
                   .HasForeignKey(op => op.order_id);
        }
    }
}
