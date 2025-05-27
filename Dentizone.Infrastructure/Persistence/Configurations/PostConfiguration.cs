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

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id)
                .IsRequired();
            builder.Property(p => p.title)
                .HasMaxLength(255)
                .IsRequired();
            builder.Property(p => p.description)
                .HasMaxLength(-1)
                .IsRequired();
            builder.Property(p => p.price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");
            builder.Property(p => p.expire_date)
                .IsRequired(false);
            builder.Property(p => p.condition)
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(p => p.status)
                .IsRequired();
            builder.Property(p => p.seller_id)
                .IsRequired();
            builder.Property(p => p.item_id)
                .IsRequired();
            builder.Property(p => p.slug)
                .HasMaxLength(255)
                .IsRequired();
            builder.Property(p => p.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()")
                .ValueGeneratedOnAdd()
                .IsRequired();
            builder.Property(p => p.UpdatedAt)
                .IsRequired();
            builder.Property(p => p.IsDeleted)
                .IsRequired();

    

        }
    }
}

