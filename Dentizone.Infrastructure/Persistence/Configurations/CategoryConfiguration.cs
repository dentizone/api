using Dentizone.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dentizone.Infrastructure.Persistence.Configurations
{
    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);


            builder.Property(c => c.Id)
                .IsRequired();


            builder.Property(c => c.Name)
                .HasMaxLength(255)
                .IsRequired();


            builder.Property(c => c.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()")
                .ValueGeneratedOnAdd()
                .IsRequired();


            builder.Property(c => c.UpdatedAt)
                .IsRequired();

            builder.Property(c => c.IsDeleted)
                .IsRequired();


            // One-to-Many: Category to SubCategories
            builder.HasMany(c => c.SubCategories)
                .WithOne(sc => sc.Category)
                .HasForeignKey(sc => sc.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}