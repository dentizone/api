using Dentizone.Domain.Enums;

namespace Dentizone.Application.Interfaces
{
    public interface IShippingService
    {
        Task UpdateItemShipmentStatusAsync(string orderItemId, ShipmentActivityStatus newStatus,
            string? comments);
    }
}