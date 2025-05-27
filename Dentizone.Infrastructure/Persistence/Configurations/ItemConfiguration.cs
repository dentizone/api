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
    internal class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.ToTable("Items")
                .HasKey(i => i.Id);
            builder.Property(i => i.category_id)
                .IsRequired();
            builder.Property(i => i.sub_category_id)
                .IsRequired();
            builder.HasOne(i => i.Category)
                .WithMany(c => c.Items)
                .HasForeignKey(i => i.category_id);
            builder.HasOne(i => i.SubCategory)
                .WithMany(sc => sc.Items)
                .HasForeignKey(i => i.sub_category_id);






        }
    }
}
