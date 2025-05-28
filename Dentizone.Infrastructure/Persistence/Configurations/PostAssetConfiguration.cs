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
    internal class PostAssetConfiguration : IEntityTypeConfiguration<PostAsset>
    {
        public void Configure(EntityTypeBuilder<PostAsset> builder)
        {
            builder.HasKey(pa => pa.Id);

            builder.Property(pa => pa.PostId)
                .IsRequired();

            builder.Property(pa => pa.AssetId)
                .IsRequired();

            builder.Property(pa => pa.DisplayOrder)
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
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(pa => pa.Asset)
                .WithMany(a => a.PostAssets)
                .HasForeignKey(pa => pa.AssetId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
