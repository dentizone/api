using Dentizone.Domain;
using Dentizone.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dentizone.Infrastructure.Persistence.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(u => u.FullName)
                   .IsRequired()
                   .HasMaxLength(255);

            builder.Property(u => u.AcademicYear)
                   .IsRequired();

            builder.Property(u => u.UniversityId)
                   .IsRequired();


            builder.Property(u => u.NationalId);

            builder.Property(u => u.KycStatus)
                   .IsRequired()
                   .HasConversion<string>();

            builder.Property(u => u.Status)
                   .IsRequired()
                   .HasConversion<string>();

            builder.Property(u => u.CreatedAt)
                   .HasDefaultValueSql(SQLCommon.Date)
                   .ValueGeneratedOnAdd();


            builder.Property(u => u.UpdatedAt)
            .IsRequired();

            builder.Property(u => u.IsDeleted)
                   .IsRequired()
                   .HasDefaultValue(false);


            // Relationships
            // One-to-One: User to Wallet
            builder.HasOne(u => u.Wallet)
                            .WithOne(w => w.User)
                            .HasForeignKey<Wallet>(w => w.UserId);

            // One-to-Many: User to UserAssets
            builder.HasMany(u => u.UserAssets)
                   .WithOne(ua => ua.User)
                   .HasForeignKey(ua => ua.UserId);

            // One-to-Many: User to UserAssets
            builder.HasMany(u => u.UserAssets)
                   .WithOne(ua => ua.User)
                   .HasForeignKey(ua => ua.UserId);

            // One-to-One: User to University
            builder.HasOne(u => u.University)
                   .WithMany(un => un.Users)
                   .HasForeignKey(u => u.UniversityId);



        }
    }
}
