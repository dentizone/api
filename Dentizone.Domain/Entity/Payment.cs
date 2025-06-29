using Dentizone.Domain.Enums;
using Dentizone.Domain.Interfaces;

namespace Dentizone.Domain.Entity
{
    public class Payment : IBaseEntity, IUpdatable
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string OrderId { get; set; } = string.Empty;
        public string BuyerId { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public PaymentMethod Method { get; set; }
        public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
        public DateTime CreatedAt { get; set; }


        // Navigation properties
        public virtual AppUser Buyer { get; set; } = new();
        public virtual Order Order { get; set; } = new();

        public virtual ICollection<SalesTransaction> SalesTransactions { get; set; } = new List<SalesTransaction>();
        public DateTime UpdatedAt { get; set; }
    }
}