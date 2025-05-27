using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Dentizone.Infrastructure.Models;

namespace Dentizone.Infrastructure.Persistence.Configurations
{
    class AssetConfiguration : IEntityTypeConfiguration<Asset>
    {
        /// <summary>
        /// Configures the Entity Framework Core mapping for the <see cref="Asset"/> entity, including property constraints, value conversions, default values, and relationships.
        /// </summary>
        /// <param name="builder">The builder used to configure the <see cref="Asset"/> entity type.</param>
        public void Configure(EntityTypeBuilder<Asset> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Url)
                .IsRequired()
                .HasMaxLength(2048);

            builder.Property(a => a.Size)
                .IsRequired();

            builder.Property(a => a.Type)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(a => a.CreatedAt)
                .ValueGeneratedOnAdd()
                .IsRequired()
                .HasDefaultValueSql(SQLCommon.Date);

            builder.Property(a => a.UpdatedAt)
          .IsRequired();

            builder.Property(a => a.Status)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(a => a.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false);


            // --- Relationships ---



            // One-to-Many: Asset to UserAssets
            builder.HasMany(a => a.UserAssets)
                   .WithOne(ua => ua.Asset) 
                   .HasForeignKey(ua => ua.AssetId)
                   .OnDelete(DeleteBehavior.Cascade); 
        }
    }

}
