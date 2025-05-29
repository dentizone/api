using Dentizone.Domain.Entity;
using Dentizone.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dentizone.Infrastructure.Persistence.Configurations
{
    internal class SalesTransactionConfiguration : IEntityTypeConfiguration<SalesTransaction>
    {
        public void Configure(EntityTypeBuilder<SalesTransaction> builder)
        {
            builder
                .Property(sale => sale.Id).IsRequired();
            builder
                .Property(sale => sale.PaymentId).IsRequired();
            builder
                .Property(sale => sale.WalletId).IsRequired();

            builder.Property(sale => sale.Amount)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(sale => sale.Status)
                .HasDefaultValue(SaleStatus.Pending)
                .HasConversion<string>();


            builder.HasOne(sale => sale.Payment)
                .WithMany(payment => payment.SalesTransactions);


            builder
                .HasOne(sale => sale.Wallet)
                .WithMany(wallet => wallet.SalesTransactions)
                .HasForeignKey(sale => sale.WalletId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}