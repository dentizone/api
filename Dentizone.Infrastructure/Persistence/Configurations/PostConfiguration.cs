using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dentizone.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Dentizone.Infrastructure.Persistence.Configurations
{
    internal class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.ToTable("Posts")
                .HasKey(p => p.Id);
            builder.Property(p => p.Id)
                .IsRequired();
            builder.Property(p => p.title)
                .IsRequired();
            builder.Property(p => p.price)
                .IsRequired();
            builder.Property(p => p.description)
                .IsRequired();
            builder.Property(p => p.condition)
                .IsRequired();
            builder.Property(p => p.status)
                .IsRequired();
            builder.Property(p => p.IsDeleted)
                .IsRequired();
            builder.Property(p => p.seller_id)
                .IsRequired();
            builder.Property(p => p.item_id)
                .IsRequired();
            builder.Property(p => p.slug)
                .IsRequired();
             builder.Property(p => p.expire_date)
                .IsRequired(false); // Assuming expire_date can be null


            builder.Property(p => p.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
            builder.Property(p => p.UpdatedAt)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");


            // Relationships
            builder.HasOne(p => p.item)
                .WithMany(i => i.Posts)
                .HasForeignKey(p => p.item_id);
            builder.HasOne(p => p.users)
                .WithMany(u => u.posts)
                .HasForeignKey(p => p.seller_id);
         
            //builder.HasMany(p=>p.questions)
            //    .WithOne(q => q.post)
            //    .HasForeignKey(q => q.post_id);

            //builder.HasMany(p => p.PostAssets)
            //    .WithOne(pa => pa.post)
            //    .HasForeignKey(pa => pa.post_id);


            //   builder.HasOne(p=>p.pickupinfo)
            //       .WithMany(pi => pi.posts)
            //      .HasForeignKey(p => p.pickupinfo_id)



        }
    }
}



