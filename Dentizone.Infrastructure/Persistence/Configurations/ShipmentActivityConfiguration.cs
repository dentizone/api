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


            builder.Property(sa => sa.Status)
                .IsRequired();


            builder.Property(sa => sa.ActivityDescription)
                .HasColumnType("text");

            builder.Property(sa => sa.CreatedAt)
                .IsRequired()
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql(SqlCommon.Date);


            builder.HasOne(sa => sa.Order)
                .WithMany(o => o.ShipmentActivities)
                .HasForeignKey(sa => sa.OrderId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}