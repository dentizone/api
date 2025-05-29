using Dentizone.Domain.Entity;
using Dentizone.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dentizone.Infrastructure.Persistence.Configurations
{
    internal class ReviewUxConfiguration : IEntityTypeConfiguration<ReviewUx>
    {
        public void Configure(EntityTypeBuilder<ReviewUx> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Id)
                .IsRequired();
            builder.Property(r => r.UserId)
                .IsRequired();
            builder.Property(r => r.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false);


            builder.Property(r => r.Text)
                .HasMaxLength(100);

            builder.Property(r => r.Stars)
                .HasDefaultValue(5);


            builder.Property(r => r.OrderId)
                .IsRequired();

            builder.Property(r => r.Status)
                .HasDefaultValue(ReviewStatus.PENDING)
                .HasConversion<string>();


            builder.Property(r => r.CreatedAt)
                .HasDefaultValueSql(SqlCommon.Date)
                .ValueGeneratedOnAdd();

            builder.Property(r => r.UpdatedAt)
                .IsRequired();

            builder.HasOne(r => r.User)
                .WithMany(u => u.UXReviews)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.NoAction);


            builder.HasOne(r => r.Order)
                .WithOne(r => r.ReviewUx)
                .HasForeignKey<ReviewUx>(r => r.OrderId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}