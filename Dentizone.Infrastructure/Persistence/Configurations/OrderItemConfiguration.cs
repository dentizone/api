using Dentizone.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dentizone.Infrastructure.Persistence.Configurations
{
    internal class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(oi => oi.Id);


            builder.Property(oi => oi.PostId).IsRequired();
            builder.Property(oi => oi.OrderId).IsRequired();


            builder.HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId);


            builder.HasOne(oi => oi.Post)
                .WithMany()
                .HasForeignKey(oi => oi.PostId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}