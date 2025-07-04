using Dentizone.Application.DTOs.Shipping;
using Dentizone.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Dentizone.Application.Interfaces
{
    public interface IShippingService
    {
        Task UpdateItemShipmentStatusAsync(CreateShipmentStatusDto shipmentStatus);
    }
}