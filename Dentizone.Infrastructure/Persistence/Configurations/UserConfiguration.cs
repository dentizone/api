using Dentizone.Domain;
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

                     builder.Property(u => u.Email)
                            .IsRequired()
                            .HasMaxLength(255);


                     builder.Property(u => u.AcademicYear)
                            .IsRequired()
                            .HasMaxLength(50);

                     builder.Property(u => u.UniversityId)
                            .IsRequired();

                     builder.Property(u => u.PhoneNumber)
                            .IsRequired()
                            .HasMaxLength(20);

                     builder.Property(u => u.NationalId);

                     builder.Property(u => u.KycStatus)
                            .IsRequired()
                            .HasConversion<string>();

                     builder.Property(u => u.Status)
                            .IsRequired()
                            .HasConversion<string>();

                     builder.Property(u => u.CreatedAt)
                   .IsRequired()
                            .HasDefaultValueSql("GETDATE()");


                     builder.Property(u => u.UpdatedAt)
                            .IsRequired()
                            .HasDefaultValueSql("GETDATE()")
                            .ValueGeneratedOnAddOrUpdate();



              }
       }
}
