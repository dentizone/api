using Dentizone.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dentizone.Infrastructure.Persistence.Configurations
{
    internal class WithdrawalRequestConfiguration : IEntityTypeConfiguration<WithdrawalRequest>
    {
        public void Configure(EntityTypeBuilder<WithdrawalRequest> builder)
        {

            builder.ToTable("WithdrawalRequests")
                   .HasKey(wr => wr.Id);
            builder.Property(wr => wr.Id)
                   .IsRequired();
            builder.Property(wr => wr.wallet_id)
                   .IsRequired();
            builder.Property(wr => wr.amount)
                   .IsRequired();
            builder.Property(wr => wr.ProcessingFee)
                   .IsRequired()
                   .HasMaxLength(100);
            builder.Property(wr => wr.status)
                   .IsRequired();

            builder.Property(wr => wr.CreatedAt)
                   .IsRequired()
                   .HasDefaultValueSql(SqlCommon.Date);
            builder.Property(wr => wr.UpdatedAt)
                   .IsRequired()
                   .HasDefaultValueSql(SqlCommon.Date);
            builder
                .HasOne(wr => wr.Wallet)
                .WithMany(w => w.WithdrawalRequests)
                .HasForeignKey(wr => wr.wallet_id);

        }
    }
}