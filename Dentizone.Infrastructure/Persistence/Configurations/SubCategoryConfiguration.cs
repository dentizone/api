using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Dentizone.Infrastructure.Models;

namespace Dentizone.Infrastructure.Persistence.Configurations
{
    class SubCategoryConfiguration : IEntityTypeConfiguration<SubCategory>
    {
        /// <summary>
        /// Configures the entity schema for the <see cref="SubCategory"/> model, including property constraints and relationships.
        /// </summary>
        /// <param name="builder">The builder used to configure the <see cref="SubCategory"/> entity type.</param>
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
                   .HasForeignKey(sc => sc.CategoryId);

        }
    }

}
