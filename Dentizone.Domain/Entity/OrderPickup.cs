using Dentizone.Domain.Interfaces;

namespace Dentizone.Domain.Entity
{
    public class OrderPickup : IBaseEntity
    {
        public string OrderId { get; set; }
        public Order Order { get; set; }
        public string PickupId { get; set; }
        public PickupInfo Pickup { get; set; }
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}