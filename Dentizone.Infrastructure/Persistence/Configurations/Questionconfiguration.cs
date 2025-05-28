using Dentizone.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dentizone.Infrastructure.Persistence.Configurations
{
       internal class Questionconfiguration : IEntityTypeConfiguration<Question>
       {
              public void Configure(EntityTypeBuilder<Question> builder)
              {
                     builder.HasKey(i => i.Id);

                     builder.Property(i => i.Id).IsRequired();
                     builder.Property(i => i.PostId).IsRequired();
                     builder.Property(i => i.AskerId).IsRequired();
                     builder.Property(i => i.Text).IsRequired();
                     builder.Property(i => i.AddedAt).IsRequired();
                     builder.Property(i => i.Status).IsRequired();
                     builder.Property(i => i.IsDeleted).IsRequired();

                     builder.HasOne(q => q.User)
                         .WithMany(u => u.Questions)
                         .HasForeignKey(q => q.AskerId);

                     builder.HasOne(q => q.Post)
                        .WithMany(p => p.Questions)
                        .HasForeignKey(q => q.PostId);

              }
       }
}
