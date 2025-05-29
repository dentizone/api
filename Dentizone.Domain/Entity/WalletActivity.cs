using Dentizone.Application.Interfaces;
using Dentizone.Domain.Enums;

namespace Dentizone.Infrastructure.Models
{
    internal class WalletActivity : IBaseEntity
    {
        public string Id { get; set; }
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
