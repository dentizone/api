using Dentizone.Application.Interfaces;

namespace Dentizone.Infrastructure.Models
{
    internal class OrderPickup : IBaseEntity
    {
        public string order_id { get; set; }
        public Order order { get; set; }
        public string pickup_id { get; set; }
        public PickupInfo pickup { get; set; }
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
