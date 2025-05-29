using Dentizone.Application.Interfaces;
using Dentizone.Domain.Enums;

namespace Dentizone.Infrastructure.Models
{
       internal class WithdrawalRequest : IBaseEntity
       {
              public string Id { get; set; }
              public string WalletId { get; set; }
              public decimal Amount { get; set; }
              public WithdrawalRequestStatus Status { get; set; } // e.g., Pending, Approved, Rejected
              public decimal? ProcessingFee { get; set; } // Optional: fee charged for processing the withdrawal


              public string? AdminNotes { get; set; }


              public DateTime CreatedAt { get; set; }
              public DateTime UpdatedAt { get; set; }
              public bool IsDeleted { get; set; }

              public Wallet Wallet { get; set; } // Navigation property to the Wallet entity
       }
}