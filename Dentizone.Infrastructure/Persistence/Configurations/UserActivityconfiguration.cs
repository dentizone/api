using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dentizone.Infrastructure.Models
{
    internal class UserActivityConfiguration : IEntityTypeConfiguration<UserActivity>
    {
        /// <summary>
        /// Configures the entity model for <see cref="UserActivity"/> in Entity Framework Core, defining property constraints, default values, and relationships.
        /// </summary>
        /// <param name="builder">The builder used to configure the <see cref="UserActivity"/> entity.</param>
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
                   .HasDefaultValueSql(SQLCommon.Date);

            builder.Property(ua => ua.UpdatedAt)
                     .IsRequired();

            builder.Property(ua => ua.DetectedAt)
                   .IsRequired();

            builder.Property(ua => ua.ActivityType)
          .IsRequired()
          .HasMaxLength(48);

            builder.Property(ua => ua.IpAddress)
                   .HasMaxLength(45);



            builder.HasOne(ua => ua.User)
                   .WithMany(u => u.UserActivities)
                   .HasForeignKey(ua => ua.UserId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
