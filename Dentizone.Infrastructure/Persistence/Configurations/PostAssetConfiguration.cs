using Dentizone.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dentizone.Infrastructure.Persistence.Configurations
{
       internal class PostAssetConfiguration : IEntityTypeConfiguration<PostAsset>
       {
              public void Configure(EntityTypeBuilder<PostAsset> builder)
              {
                     builder.HasKey(pa => pa.Id);

                     builder.Property(pa => pa.PostId)
                            .IsRequired();

                     builder.Property(pa => pa.AssetId)
                            .IsRequired();


                     builder.Property(w => w.CreatedAt)
                            .HasDefaultValueSql(SqlCommon.Date)
                            .ValueGeneratedOnAdd();

                     builder.Property(pa => pa.IsDeleted)
                            .IsRequired()
                            .HasDefaultValue(false);

                     builder.HasOne(pa => pa.Post)
                            .WithMany(p => p.PostAssets)
                            .HasForeignKey(pa => pa.PostId)
                            .OnDelete(DeleteBehavior.NoAction).IsRequired();

                     builder.HasOne(pa => pa.Asset)
                            .WithMany(a => a.PostAssets)
                            .HasForeignKey(pa => pa.AssetId)
                            .OnDelete(DeleteBehavior.NoAction);
              }
       }
}