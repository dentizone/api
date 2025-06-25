using Dentizone.Domain.Enums;
using Dentizone.Domain.Interfaces;

namespace Dentizone.Domain.Entity
{
    public class WithdrawalRequest : IBaseEntity, IUpdatable
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string WalletId { get; set; }
        public decimal Amount { get; set; }
        public WithdrawalRequestStatus Status { get; set; } = WithdrawalRequestStatus.Pending;
        public decimal? ProcessingFee { get; set; } = 0;


        public string? AdminNotes { get; set; }


        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Wallet Wallet { get; set; } // Navigation property to the Wallet entity
    }
}