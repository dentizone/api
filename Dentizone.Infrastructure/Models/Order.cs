using Dentizone.Application.Interfaces;
using Dentizone.Domain;
using Dentizone.Domain.Enums;

namespace Dentizone.Infrastructure.Models
{
    internal class Order : IBaseEntity
    {
        public string BuyerId { get; set; }
        public AppUser Buyer { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime PlacedAt { get; set; }

        public decimal CommissionAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime CompletedAt { get; set; }
        public virtual ShipInfo ShipInfo { get; set; }


        public virtual ICollection<OrderPickup> OrderPickups { get; set; } = new List<OrderPickup>();

        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }

    }
}
