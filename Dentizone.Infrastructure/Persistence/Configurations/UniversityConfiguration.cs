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
    internal class UniversityConfiguration : IEntityTypeConfiguration<University>
    {
        public void Configure(EntityTypeBuilder<University> builder)
        {
            builder.HasKey(u => u.Id);


            builder.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(u => u.IsSupported)
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(u => u.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(u => u.CreatedAt)
                .HasDefaultValueSql(SQLCommon.Date)
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(u => u.UpdatedAt)
                .IsRequired();


            builder.Property(u => u.Domain)
                .IsRequired()
                .HasMaxLength(48);

            // --- Relationships ---

            // One-to-Many: University to Users
            builder.HasMany(u => u.Users)
                   .WithOne(usr => usr.University)
                   .HasForeignKey(usr => usr.UniversityId);
        }
    }

}
