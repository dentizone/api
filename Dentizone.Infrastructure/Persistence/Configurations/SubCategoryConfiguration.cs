using Dentizone.Domain.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Dentizone.Infrastructure.Persistence.Configurations
{
    class SubCategoryConfiguration : IEntityTypeConfiguration<SubCategory>
    {
        public void Configure(EntityTypeBuilder<SubCategory> builder)
        {
            builder.HasKey(sc => sc.Id);


            builder.Property(sc => sc.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(sc => sc.CategoryId)
                .IsRequired();

            builder.Property(sc => sc.CreatedAt)
                .IsRequired();

            builder.Property(sc => sc.UpdatedAt)
                .IsRequired();

            builder.Property(sc => sc.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false);

            // --- Relationships ---

            // Many-to-One: SubCategory appears in manyt Category
            builder.HasOne(sc => sc.Category)
                .WithMany(c => c.SubCategories)
                .HasForeignKey(sc => sc.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}