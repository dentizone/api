using Dentizone.Application.Interfaces.Order;
using Dentizone.Domain.Enums;
using Dentizone.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dentizone.Presentaion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShippingController(IShippingService shipmentActivity) : ControllerBase
    {
        [HttpPut]
        public async Task<IActionResult> UpdateItemShipmentStatus(string orderItemId, ShipmentActivityStatus newStatus,
            string? comment)
        {
            await shipmentActivity.UpdateItemShipmentStatusAsync(orderItemId, newStatus, comment);
            return Ok();
        }
    }
}