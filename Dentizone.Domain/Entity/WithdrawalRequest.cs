using Dentizone.Domain.Enums;
using Dentizone.Domain.Interfaces;

namespace Dentizone.Domain.Entity
{
    public class WithdrawalRequest : IBaseEntity
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string WalletId { get; set; }
        public decimal Amount { get; set; }
        public WithdrawalRequestStatus Status { get; set; } // e.g., Pending, Approved, Rejected
        public decimal? ProcessingFee { get; set; }         // Optional: fee charged for processing the withdrawal


        public string? AdminNotes { get; set; }


        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }

        public Wallet Wallet { get; set; } // Navigation property to the Wallet entity
    }
}