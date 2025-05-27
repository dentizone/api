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
    internal class Questionconfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.HasKey(i => i.id);

            builder.Property(i => i.id).IsRequired();
            builder.Property(i => i.post_id).IsRequired();
            builder.Property(i => i.asker_id).IsRequired();
            builder.Property(i => i.text).IsRequired();
            builder.Property(i => i.added_at).IsRequired();
            builder.Property(i => i.status).IsRequired();
            builder.Property(i => i.is_deleted).IsRequired();

            builder.HasOne(q => q.user)
                .WithMany(u => u.questions) 
                .HasForeignKey(q => q.asker_id);

            builder.HasOne(q => q.Post)
               .WithMany(p => p.questions) 
               .HasForeignKey(q => q.post_id);

        }
    }
}
