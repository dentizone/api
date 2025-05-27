using Dentizone.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dentizone.Infrastructure.Persistence.Configurations;

internal class UserAssetConfiguration : IEntityTypeConfiguration<UserAsset>
{
    /// <summary>
    /// Configures the Entity Framework Core model for the <see cref="UserAsset"/> entity, including property requirements, default values, and relationships to <see cref="User"/> and <see cref="Asset"/>.
    /// </summary>
    /// <param name="builder">The builder used to configure the <see cref="UserAsset"/> entity type.</param>
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
        // // Many-to-One: UserAsset to Asset
        builder.HasOne(ua => ua.Asset)
                     .WithMany(a => a.UserAssets)
             .HasForeignKey(ua => ua.AssetId);
    }
}