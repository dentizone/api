using Dentizone.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dentizone.Infrastructure.Models
{
    internal class UserActivityConfiguration : IEntityTypeConfiguration<UserActivity>
    {
        public void Configure(EntityTypeBuilder<UserActivity> builder)
        {
            builder.HasKey(ua => ua.Id);


            builder.Property(ua => ua.UserId)
                .IsRequired();

            builder.Property(ua => ua.FingerprintToken)
                .IsRequired()
                .HasMaxLength(512);

            builder.Property(ua => ua.Device)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(ua => ua.UserAgent)
                .IsRequired()
                .HasMaxLength(1024);

            builder.Property(ua => ua.CreatedAt)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql(SqlCommon.Date);

            builder.Property(ua => ua.UpdatedAt)
                .IsRequired();

            builder.Property(ua => ua.DetectedAt)
                .IsRequired();

            builder.Property(ua => ua.ActivityType)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(ua => ua.IpAddress)
                .HasMaxLength(45);


            builder.HasOne(ua => ua.User)
                .WithMany(u => u.UserActivities)
                .HasForeignKey(ua => ua.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}