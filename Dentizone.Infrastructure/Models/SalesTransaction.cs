using Dentizone.Application.Interfaces;

namespace Dentizone.Infrastructure.Models
{
    internal class SalesTransaction : IBaseEntity
    {
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }

        public string WalletId { get; set; }
        public string PaymentId { get; set; }
        public decimal Amount { get; set; }

        public SaleStatus Status { get; set; }

        // Navigation properties
        public virtual Wallet Wallet { get; set; }
        public virtual Payment Payment { get; set; }


    }
}
