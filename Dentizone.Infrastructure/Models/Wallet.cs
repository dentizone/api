using Dentizone.Application.Interfaces;
using Dentizone.Domain;
using Dentizone.Domain.Enums;

namespace Dentizone.Infrastructure.Models
{
    internal class Wallet : IBaseEntity
    {
        public string Id { get; set; }


        public decimal Balance { get; set; }
        public UserWallet Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }


        // Relationship: One Wallet to One User
        public virtual AppUser User { get; set; }
        public string UserId { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<WalletActivity> WalletActivities { get; set; } = new List<WalletActivity>();
        public ICollection<SalesTransaction> SalesTransactions { get; set; } = new List<SalesTransaction>();

    }
}
