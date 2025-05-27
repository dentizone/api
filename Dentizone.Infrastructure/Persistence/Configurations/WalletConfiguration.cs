using Dentizone.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dentizone.Infrastructure.Persistence.Configurations;

internal class WalletConfiguration : IEntityTypeConfiguration<Wallet>
{
    public void Configure(EntityTypeBuilder<Wallet> builder)
    {
        builder.HasKey(w => w.Id);


        builder.Property(w => w.UserId)
               .IsRequired();

        builder.Property(w => w.Balance)
               .IsRequired()
               .HasColumnType("decimal(18, 4)");

        builder.Property(w => w.Status)
               .IsRequired()
               .HasConversion<string>();

        builder.Property(w => w.CreatedAt)
               .HasDefaultValueSql(SQLCommon.Date)
               .ValueGeneratedOnAdd();

        builder.Property(w => w.UpdatedAt).IsRequired();



        // Relationships

        // One-to-One: Wallet to User is 
        builder.HasOne(w => w.User)
               .WithOne(u => u.Wallet)
               .HasForeignKey<Wallet>(w => w.UserId)
               .OnDelete(DeleteBehavior.NoAction);

    }
}