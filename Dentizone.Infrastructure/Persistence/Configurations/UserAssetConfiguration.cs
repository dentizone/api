using Dentizone.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dentizone.Infrastructure.Persistence.Configurations;

internal class UserAssetConfiguration : IEntityTypeConfiguration<UserAsset>
{
    public void Configure(EntityTypeBuilder<UserAsset> builder)
    {
        builder.HasKey(ua => ua.Id);

        builder.Property(ua => ua.UserId)
               .IsRequired();

        builder.Property(ua => ua.AssetId)
               .IsRequired();

        builder.Property(ua => ua.Type)
               .IsRequired()
               .HasConversion<string>();

        builder.Property(ua => ua.CreatedAt)
               .HasDefaultValueSql(SQLCommon.Date)
               .ValueGeneratedOnAdd();

        builder.Property(ua => ua.UpdatedAt)
                .IsRequired();
        builder.Property(ua => ua.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false);

        // --- Relationships ---

        // Many-to-One: UserAsset to User
        builder.HasOne(ua => ua.User)
               .WithMany(u => u.UserAssets)
               .HasForeignKey(ua => ua.UserId);
    }
}