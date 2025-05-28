using Dentizone.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
                .IsRequired()
                .HasConversion<string>();


            builder.Property(p => p.status)
                .IsRequired();
            builder.Property(p => p.SellerId)
                .IsRequired();
            builder.Property(p => p.item_id)
                .IsRequired();
            builder.Property(p => p.slug)
                .HasMaxLength(255)
                .IsRequired();
            builder.Property(p => p.CreatedAt)
                .HasDefaultValueSql(SQLCommon.Date)
                .ValueGeneratedOnAdd();


            builder.Property(p => p.UpdatedAt)
                .IsRequired();
            builder.Property(p => p.IsDeleted)
                .IsRequired();


            // Every post must have a pickup info, only one

            builder.HasOne(p => p.pickupinfo)
                   .WithOne(pi => pi.Post)
                     .HasForeignKey<PickupInfo>(pi => pi.PostId);
            // Every post must have a seller, only one
            builder.HasOne(p => p.Seller)
                   .WithMany(u => u.Posts)
                   .HasForeignKey(p => p.SellerId);



        }
    }
}

