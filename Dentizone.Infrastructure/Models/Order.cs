using Dentizone.Application.Interfaces;
using Dentizone.Domain;
using Dentizone.Domain.Enums;

namespace Dentizone.Infrastructure.Models
{
    internal class Order : IBaseEntity
    {
        public string buyer_id { get; set; }
        public AppUser user { get; set; }
        public OrderStatus status { get; set; }
        public DateTime placed_at { get; set; }

        public decimal commission_amount { get; set; }
        public decimal total_amount { get; set; }
        public DateTime completed_at { get; set; }
        public DateTime updated_at { get; set; }
        public ShipInfo ShipInfo { get; set; }


        // @Nouran: Every order has Many Order Pickups
        public ICollection<OrderPickup> OrderPickups { get; set; } = new List<OrderPickup>();

        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }

    }
}
