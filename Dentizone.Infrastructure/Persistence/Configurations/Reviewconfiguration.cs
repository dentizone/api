using Dentizone.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dentizone.Infrastructure.Persistence.Configurations
{
    internal class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Id)
                .IsRequired();

            builder.Property(r => r.UserId)
                .IsRequired();

            builder.Property(r => r.OrderId)
                .IsRequired();

            builder.Property(r => r.Stars)
                .IsRequired();

            builder.Property(r => r.Text)
                   .HasColumnType("text");

            builder.Property(r => r.IsDeleted)
                     .HasDefaultValue(false);

            builder.Property(r => r.CreatedAt);

            builder.Property(r => r.UpdatedAt);


            builder.HasOne(r => r.User)
                   .WithMany(u => u.Reviews)
                   .HasForeignKey(r => r.UserId);

            builder.HasOne(r => r.Order)
                   .WithOne(o => o.Review)
                   .HasForeignKey<Review>(r => r.OrderId);
        }

    }
}
