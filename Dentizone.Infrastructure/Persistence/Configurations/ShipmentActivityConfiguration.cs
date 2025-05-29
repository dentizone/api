using Dentizone.Domain.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Dentizone.Infrastructure.Persistence.Configurations
{
    internal class ShipmentActivityConfiguration : IEntityTypeConfiguration<ShipmentActivity>
    {
        public void Configure(EntityTypeBuilder<ShipmentActivity> builder)
        {
            builder.HasKey(sa => sa.Id);

            builder.Property(sa => sa.Id)
                .IsRequired();


            builder.Property(sa => sa.OrderId)
                .IsRequired();

            builder.Property(sa => sa.ShippedBy)
                .IsRequired();


            builder.Property(sa => sa.Status)
                .IsRequired();


            builder.Property(sa => sa.AssignedBy)
                .IsRequired();


            builder.Property(sa => sa.ActivityDescription)
                .HasColumnType("text");

            builder.Property(sa => sa.CreatedAt)
                .IsRequired()
                .HasColumnType("timestamp")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");


            builder.HasOne(sa => sa.Order)
                .WithMany(o => o.ShipmentActivities)
                .HasForeignKey(sa => sa.OrderId);
        }
    }
}