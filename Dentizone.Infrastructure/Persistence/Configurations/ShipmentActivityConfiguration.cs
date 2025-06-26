using Dentizone.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dentizone.Infrastructure.Persistence.Configurations
{
    internal class ShipmentActivityConfiguration : IEntityTypeConfiguration<ShipmentActivity>
    {
        public void Configure(EntityTypeBuilder<ShipmentActivity> builder)
        {
            builder.HasKey(sa => sa.Id);

            builder.Property(sa => sa.Id)
                   .IsRequired();


            builder.Property(sa => sa.ItemId)
                   .IsRequired();


            builder.Property(sa => sa.Status)
                   .IsRequired();


            builder.Property(sa => sa.ActivityDescription)
                   .HasColumnType("text");

            builder.Property(sa => sa.CreatedAt)
                   .IsRequired()
                   .ValueGeneratedOnAdd()
                   .HasDefaultValueSql(SqlCommon.Date);


            builder.HasOne(sa => sa.Item)
                   .WithMany(o => o.ShipmentActivities)
                   .HasForeignKey(sa => sa.ItemId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}