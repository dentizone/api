using Dentizone.Domain.Interfaces;

namespace Dentizone.Domain.Entity
{
    public class Order : IBaseEntity, IDeletable, IUpdatable
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string BuyerId { get; set; } = string.Empty;
        public AppUser Buyer { get; set; }
        public decimal CommissionAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime? CompletedAt { get; set; }

        public bool IsReviewed { get; set; } = false;
        public virtual ShipInfo ShipInfo { get; set; }
        public virtual ICollection<OrderPickup> OrderPickups { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<OrderStatus> OrderStatuses { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public virtual Review Review { get; set; }
    }
}