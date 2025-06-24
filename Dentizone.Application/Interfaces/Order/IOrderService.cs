using Dentizone.Application.DTOs.Order;

namespace Dentizone.Application.Interfaces.Order
{
    public interface IOrderService
    {
        Task<string> CreateOrderAsync(CreateOrderDto createOrderDto, string buyerId);
        Task<OrderViewDto> GetOrderByIdAsync(string orderId, string buyerId);
        Task<List<OrderViewDto>> GetOrdersByBuyerAsync(string buyerId);
        Task<OrderViewDto?> CancelOrderAsync(string orderId, string userId);
        Task CompleteOrder(string orderId);
        Task<IReadOnlyCollection<OrderViewDto>> GetOrders(int Page);
    }
}