using Dentizone.Domain.Entity;
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
            .HasDefaultValueSql(SqlCommon.Date)
            .ValueGeneratedOnAdd();

        builder.Property(w => w.UpdatedAt)
            .IsRequired();

        builder.Property(w => w.IsDeleted)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(w => w.UpdatedAt).IsRequired();


        // Relationships

        // One-to-One: Wallet to User is 
        builder.HasOne(w => w.User)
            .WithOne(u => u.Wallet)
            .HasForeignKey<Wallet>(w => w.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        // One-to-Many: Wallet to WalletActivities
        builder.HasMany(w => w.WalletActivities)
            .WithOne(wa => wa.Wallet)
            .HasForeignKey(wa => wa.WalletId);


        builder.HasMany(w => w.SalesTransactions)
            .WithOne(st => st.Wallet)
            .HasForeignKey(st => st.WalletId);

        // One-to-Many: Wallet to WithdrawalRequests
        builder.HasMany(w => w.WithdrawalRequests)
            .WithOne(wr => wr.Wallet)
            .HasForeignKey(wr => wr.WalletId);
    }
}