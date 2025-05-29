using Dentizone.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dentizone.Infrastructure.Persistence.Configurations
{
    internal class OrderStatusConfiguration : IEntityTypeConfiguration<OrderStatus>
    {
        public void Configure(EntityTypeBuilder<OrderStatus> builder)
        {
            builder.HasKey(os => os.Id);

            builder.Property(os => os.Comment)
                .HasMaxLength(250);

            builder.Property(os => os.CreatedAt)
                .ValueGeneratedOnAdd()
                .IsRequired()
                .HasDefaultValueSql(SqlCommon.Date);


            builder.Property(os => os.UpdatedAt)
                .IsRequired();

            builder.Property(os => os.Status)
                .IsRequired()
                .HasConversion<string>();

            // Relationships
            builder.HasOne(os => os.Order)
                .WithMany(o => o.OrderStatuses)
                .HasForeignKey(os => os.OrderId);
        }
    }
}