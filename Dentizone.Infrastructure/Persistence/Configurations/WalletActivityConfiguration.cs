using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dentizone.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Dentizone.Infrastructure.Persistence.Configurations
{
    internal class WalletActivityConfiguration : IEntityTypeConfiguration<WalletActivity>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<WalletActivity> builder)
        {
            builder.ToTable(nameof(WalletActivity));
            builder.HasKey(wa => wa.Id);
            builder.Property(i => i.WalletId).IsRequired();
            builder.Property(i=>i.ReferenceId).IsRequired();
            builder.Property(i=>i.ReferenceType).IsRequired();
            builder.Property(i=>i.ActivityType).IsRequired();
            builder.Property(i=>i.CreatedAt).IsRequired();
            builder.HasOne(c => c.Wallet)
                .WithMany(c => c.WalletActivities)
                .HasForeignKey(c => c.WalletId);
            

                
        }
    }
}
