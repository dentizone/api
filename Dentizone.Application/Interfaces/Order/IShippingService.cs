using Dentizone.Domain.Enums;

namespace Dentizone.Application.Interfaces.Order
{
    public interface IShippingService
    {
        Task UpdateItemShipmentStatusAsync(string orderItemId, ShipmentActivityStatus newStatus,
            string? comments);
    }
}