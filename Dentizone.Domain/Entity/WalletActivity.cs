using Dentizone.Domain.Enums;
using Dentizone.Domain.Interfaces;

namespace Dentizone.Domain.Entity
{
    public class WalletActivity : IBaseEntity
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string WalletId { get; set; }
        public WalletActivityTypes ActivityType { get; set; }
        public decimal Amount { get; set; }
        public string ReferenceType { get; set; }
        public string ReferenceId { get; set; }
        public string? Description { get; set; }


        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public Wallet Wallet { get; set; }
    }
}