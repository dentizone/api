using Dentizone.Application.DTOs.Shipping;

namespace Dentizone.Application.Interfaces
{
    public interface IShippingService
    {
        Task UpdateItemShipmentStatusAsync(CreateShipmentStatusDto shipmentStatus);
    }
}