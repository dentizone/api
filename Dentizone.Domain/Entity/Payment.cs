using Dentizone.Domain.Enums;
using Dentizone.Domain.Interfaces;

namespace Dentizone.Domain.Entity
{
    public class Payment : IBaseEntity
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string OrderId { get; set; }
        public string BuyerId { get; set; }
        public decimal Amount { get; set; }
        public PaymentMethod Method { get; set; }
        public PaymentStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime ProcessedAt { get; set; }

        public bool IsDeleted { get; set; }

        // Navigation properties
        public virtual AppUser Buyer { get; set; }
        public virtual Order Order { get; set; }

        public virtual ICollection<SalesTransaction> SalesTransactions { get; set; } = new List<SalesTransaction>();
    }
}