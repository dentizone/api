using Dentizone.Domain.Enums;
using Dentizone.Domain.Interfaces;

namespace Dentizone.Domain.Entity
{
    public class Wallet : IBaseEntity, IUpdatable
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();


        public decimal Balance { get; set; } = 0.0m;
        public UserWallet Status { get; set; } = UserWallet.Active;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }


        // Relationship: One Wallet to One User
        public virtual AppUser User { get; set; } = new();
        public string UserId { get; set; } = string.Empty;

        public virtual ICollection<SalesTransaction> SalesTransactions { get; set; } = new List<SalesTransaction>();

        public virtual ICollection<WithdrawalRequest> WithdrawalRequests { get; set; } = new List<WithdrawalRequest>();
    }
}