using Dentizone.Domain.Enums;
using Dentizone.Domain.Interfaces;

namespace Dentizone.Domain.Entity
{
    public class ShipmentActivity : IBaseEntity
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string OrderId { get; set; }
        public string ShippedBy { get; set; }
        public ShipmentActivityStatus Status { get; set; }
        public string AssignedBy { get; set; }
        public string? ActivityDescription { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }


        public virtual Order Order { get; set; }
    }
}