using Dentizone.Application.DTOs.Order;
using Dentizone.Application.Interfaces.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace Dentizone.Presentaion.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderById(string orderId)
        {
            var result = await _orderService.GetOrderByIdAsync(orderId);
            if (result == null)
            {
                return NotFound("Order not found");
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto createOrderDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _orderService.CreateOrderAsync(createOrderDto, userId);
            return Ok(new { message = "Order created successfully." });
        }

        [HttpGet("my-orders")]
        public async Task<IActionResult> GetMyOrders()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orders = await _orderService.GetOrdersByBuyerAsync(userId);
            return Ok(orders);
        }

        [HttpPut("{orderId}/cancel")]
        public async Task<IActionResult> CancelOrder(string orderId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _orderService.CancelOrderAsync(orderId, userId);
            return Ok(result);
        }
    }
}