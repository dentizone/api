using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dentizone.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dentizone.Infrastructure.Persistence.Configurations
{
    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {



        /// <summary>
        /// Configures the entity schema for the <see cref="Category"/> model in Entity Framework Core.
        /// </summary>
        /// <param name="builder">The builder used to configure the <see cref="Category"/> entity type.</param>
        /// <remarks>
        /// Sets up property constraints, default values, and relationships:
        /// - Primary key on <c>Id</c>.
        /// - <c>Name</c> is required with a maximum length of 255.
        /// - <c>CreatedAt</c> is required, generated on add, with a default value of the current UTC date/time.
        /// - <c>UpdatedAt</c> and <c>IsDeleted</c> are required.
        /// - Configures one-to-many relationships to <c>SubCategories</c> and <c>Items</c> with restricted delete behavior.
        /// </remarks>
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
            // One-to-Many: Category to Items
            builder.HasMany(c => c.Items)
                   .WithOne(i => i.Category) 
                   .HasForeignKey(i => i.CategoryId) 
                   .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
