using Dentizone.Domain.Enums;

namespace Dentizone.Domain.Entity
{
    public class ShipmentActivity
    {
        public int Id { get; set; }
        public string OrderId { get; set; }
        public string ShippedBy { get; set; }
        public ShipmentActivityStatus Status { get; set; }
        public string AssignedBy { get; set; }
        public string? ActivityDescription { get; set; }
        public DateTime CreatedAt { get; set; }


        public virtual Order Order { get; set; }
    }
}