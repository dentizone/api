using Dentizone.Application.DTOs;
using Dentizone.Application.DTOs.Order;
using Dentizone.Domain.Entity;

namespace Dentizone.Application.Interfaces
{
    public interface IOrderService
    {
        Task<string> CreateOrderAsync(CreateOrderDto createOrderDto, string buyerId);
        Task<OrderViewDto> GetOrderByIdAsync(string orderId, string buyerId);
        Task<List<OrderViewDto>> GetOrdersByBuyerAsync(string buyerId);
        Task<OrderViewDto?> CancelOrderAsync(string orderId);
        Task CompleteOrder(string orderId);
        Task<PagedResultDto<OrderViewAll>> GetOrders(int page, FilterOrderDto filters);
        Task<Order> MarkOrderAsReviewed(string orderId);
        Task<IEnumerable<Order>> GetReviewedOrdersByUserId(string userId);
    }
}