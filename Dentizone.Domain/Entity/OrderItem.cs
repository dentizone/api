using Dentizone.Domain.Interfaces;

namespace Dentizone.Domain.Entity
{
    public class OrderItem : IBaseEntity
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime CreatedAt { get; set; }

        public string PostId { get; set; } = string.Empty;
        public virtual Post Post { get; set; } = new();
        public string OrderId { get; set; } = string.Empty;
        public virtual Order Order { get; set; } = new();

        public ICollection<ShipmentActivity> ShipmentActivities { get; set; } = new List<ShipmentActivity>();
    }
}