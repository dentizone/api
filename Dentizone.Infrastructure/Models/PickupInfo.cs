using Dentizone.Application.Interfaces;

namespace Dentizone.Infrastructure.Models
{
    internal class PickupInfo : IBaseEntity
    {

        public string street { get; set; }
        public string city { get; set; }
        public string post_id { get; set; }
        public Post Post { get; set; }
        public DateTime created_at { get; set; }
        public string order_id { get; set; }
        public Order Order { get; set; }

        public ICollection<OrderPickup> OrderPickups { get; set; } = new List<OrderPickup>();
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
