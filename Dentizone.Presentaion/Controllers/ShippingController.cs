using Dentizone.Application.DTOs.Shipping;
using Dentizone.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dentizone.Presentaion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "IsAdmin")]
    public class ShippingController(IShippingService shipmentActivity) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> UpdateItemShipmentStatus(CreateShipmentStatusDto shipmentStatus)
        {
            await shipmentActivity.UpdateItemShipmentStatusAsync(shipmentStatus);
            return Ok();
        }
    }
}