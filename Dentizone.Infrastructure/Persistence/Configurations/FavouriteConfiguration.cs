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
    internal class FavouriteConfiguration : IEntityTypeConfiguration<Favourite>
    {
        public void Configure(EntityTypeBuilder<Favourite> builder)
        {
            builder.HasKey(f => f.Id);

            builder.Property(f => f.UserId)
                .IsRequired();

            builder.Property(f => f.PostId)
                .IsRequired();

            builder.Property(f => f.CreatedAt)
                .IsRequired();

            builder.Property(f => f.CreatedAt)
                   .HasDefaultValueSql(SqlCommon.Date)
                   .ValueGeneratedOnAdd();

            builder.Property(f => f.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false);

            builder.HasOne(f => f.User)
                .WithMany(u => u.Favourites)
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(f => f.Post)
                .WithMany(p => p.Favourites)
                .HasForeignKey(f => f.PostId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
