using Dentizone.Application.DTOs.Order;

namespace Dentizone.Application.Interfaces.Order
{
    public interface IOrderService
    {
        Task<string> CreateOrderAsync(CreateOrderDto createOrderDTO, string buyerId);
        Task<OrderViewDto> GetOrderByIdAsync(string orderId);
        Task<List<OrderViewDto>> GetOrdersByBuyerAsync(string buyerId, int page);
        Task<OrderViewDto?> CancelOrderAsync(string orderId, string userId);
    }
}