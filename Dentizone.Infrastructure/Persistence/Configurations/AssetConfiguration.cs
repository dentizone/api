using Dentizone.Domain.Entity;
using Dentizone.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace Dentizone.Infrastructure.Persistence.Configurations
{
       class AssetConfiguration : IEntityTypeConfiguration<Asset>
       {
              public void Configure(EntityTypeBuilder<Asset> builder)
              {
                     builder.HasKey(a => a.Id);

                     builder.Property(a => a.Url)
                            .IsRequired()
                            .HasMaxLength(2048);

                     builder.Property(a => a.Size)
                            .IsRequired()
                            .HasAnnotation("Range", new RangeAttribute(1, 104857600));
                     builder.Property(a => a.Type)
                            .IsRequired()
                            .HasConversion<string>();

                     builder.Property(a => a.CreatedAt)
                            .ValueGeneratedOnAdd()
                            .IsRequired()
                            .HasDefaultValueSql(SqlCommon.Date);

                     builder.Property(a => a.UpdatedAt)
                            .IsRequired().HasDefaultValueSql(SqlCommon.Date);

                     builder.Property(a => a.Status)
                            .IsRequired()
                            .HasDefaultValue(AssetStatus.Active)
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


                     // One-to-Many: Asset to PostAssets
                     builder.HasMany(a => a.PostAssets)
                            .WithOne(pa => pa.Asset)
                            .HasForeignKey(pa => pa.AssetId);
              }
       }
}