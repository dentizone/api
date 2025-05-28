using Dentizone.Domain.Enums;
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
                     builder.Property(wr => wr.WalletId)
                            .IsRequired();
                     builder.Property(wr => wr.Amount)
                            .IsRequired();
                     builder.Property(wr => wr.ProcessingFee)
                            .HasDefaultValue(0.0m);

                     builder.Property(wr => wr.Status)
                            .HasConversion<string>()
                            .HasDefaultValue(WithdrawalRequestStatus.Pending);




                     builder.Property(wr => wr.AdminNotes)
                            .HasColumnType("text")
                            .IsRequired(false);

                     builder.Property(wr => wr.CreatedAt)
                            .IsRequired()
                            .HasDefaultValueSql(SqlCommon.Date);
                     builder.Property(wr => wr.UpdatedAt)
                            .IsRequired()
                            .HasDefaultValueSql(SqlCommon.Date);
                     builder
                         .HasOne(wr => wr.Wallet)
                         .WithMany(w => w.WithdrawalRequests)
                         .HasForeignKey(wr => wr.WalletId);

              }
       }
}