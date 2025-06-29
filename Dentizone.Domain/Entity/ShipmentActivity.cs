using Dentizone.Domain.Enums;
using Dentizone.Domain.Interfaces;

namespace Dentizone.Domain.Entity
{
    public class ShipmentActivity : IBaseEntity
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string ItemId { get; set; } = string.Empty;
        public ShipmentActivityStatus Status { get; set; }
        public string ActivityDescription { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }


        public virtual OrderItem Item { get; set; }
    }
}