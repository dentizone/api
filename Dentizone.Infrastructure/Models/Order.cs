using Dentizone.Application.Interfaces;
using Dentizone.Domain;

namespace Dentizone.Infrastructure.Models
{
    internal class Order : IBaseEntity
    {
        public string BuyerId { get; set; }
        public AppUser Buyer { get; set; }
        public decimal CommissionAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime CompletedAt { get; set; }
        public virtual ShipInfo ShipInfo { get; set; }
        public string ShipInfoId { get; set; }
        public virtual ICollection<OrderPickup> OrderPickups { get; set; } = new List<OrderPickup>();
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public virtual ICollection<OrderStatus> OrderStatuses { get; set; } = new List<OrderStatus>();
        public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public virtual Review Review { get; set; }
        public virtual ICollection<ShipmentActivity> ShipmentActivities { get; set; }
    }
}
