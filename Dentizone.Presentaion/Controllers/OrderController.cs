using Dentizone.Application.DTOs.Order;
using Dentizone.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Dentizone.Domain.Enums;


namespace Dentizone.Presentaion.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController(IOrderService orderService) : ControllerBase
    {
        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderById(string orderId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await orderService.GetOrderByIdAsync(orderId, userId);

            return Ok(result);
        }

        [HttpPost]
        [Authorize(Policy = "IsVerified")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto createOrderDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await orderService.CreateOrderAsync(createOrderDto, userId);
            return Ok(new { message = "Order created successfully." });
        }

        [HttpGet("my-orders")]
        public async Task<IActionResult> GetMyOrders()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orders = await orderService.GetOrdersByBuyerAsync(userId);
            return Ok(orders);
        }

        [HttpPut("{orderId}/cancel")]
        public async Task<IActionResult> CancelOrder(string orderId)
        {
            var result = await orderService.CancelOrderAsync(orderId);
            return Ok(result);
        }

        [HttpPut("{orderId}/confirm")]
        [Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> ConfirmOrder(string orderId)
        {
            await orderService.CompleteOrder(orderId);
            return Ok(new { message = "Order Completed Successfully" });
        }

        [HttpGet("all")]
        [Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> GetAllOrders([FromQuery] FilterOrderDto filters, [FromQuery] int page = 1)
        {
            var orders = await orderService.GetOrders(page, filters);
            return Ok(orders);
        }

        [HttpPut("{orderId}/status")]
        [Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> UpdateOrderStatus(string orderId, [FromBody] OrderStatues orderStatus)
        {
            await orderService.UpdateOrderStatus(orderId, orderStatus);
            return Ok();
        }
    }
}