using Dentizone.Domain.Enums;

namespace Dentizone.Application.DTOs.Shipping
{
    public class CreateShipmentStatusDto
    {
        public required string OrderItemId { get; set; }
        public required ShipmentActivityStatus NewStatus { get; set; }
        public string? Comment { get; set; }
    }
}