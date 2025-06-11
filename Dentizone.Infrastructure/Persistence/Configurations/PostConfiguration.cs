using Dentizone.Domain.Entity;
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
                     builder.Property(p => p.Title)
                            .HasMaxLength(255)
                            .IsRequired();
                     builder.Property(p => p.Description)
                            .HasMaxLength(-1)
                            .IsRequired();
                     builder.Property(p => p.Price)
                            .IsRequired()
                            .HasColumnType("decimal(18,2)");
                     builder.Property(p => p.ExpireDate)
                            .IsRequired(false);


                     builder.Property(p => p.Condition)
                            .IsRequired()
                            .HasConversion<string>();


                     builder.Property(p => p.Status)
                            .IsRequired();
                     builder.Property(p => p.SellerId)
                            .IsRequired();
                     builder.Property(p => p.ItemId)
                            .IsRequired();
                     builder.Property(p => p.Slug)
                            .HasMaxLength(255)
                            .IsRequired();
                     builder.Property(p => p.CreatedAt)
                            .HasDefaultValueSql(SqlCommon.Date)
                            .ValueGeneratedOnAdd();


                     builder.Property(p => p.UpdatedAt)
                            .IsRequired();
                     builder.Property(p => p.IsDeleted)
                            .IsRequired();


                     // Every post must have a pickup info, only one


                     // Every post must have a seller, only one
                     builder.HasOne(p => p.Seller)
                            .WithMany(u => u.Posts)
                            .HasForeignKey(p => p.SellerId)
                            .OnDelete(DeleteBehavior.NoAction);
              }
       }
}