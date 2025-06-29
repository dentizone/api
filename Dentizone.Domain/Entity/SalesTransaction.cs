using Dentizone.Domain.Enums;
using Dentizone.Domain.Interfaces;

namespace Dentizone.Domain.Entity
{
    public class SalesTransaction : IBaseEntity, IUpdatable
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public string WalletId { get; set; } = string.Empty;
        public string PaymentId { get; set; } = string.Empty;
        public decimal Amount { get; set; }

        public SaleStatus Status { get; set; }

        // Navigation properties
        public virtual Wallet Wallet { get; set; }
        public virtual Payment Payment { get; set; }
    }
}