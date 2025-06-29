using Dentizone.Application.DTOs;
using Dentizone.Application.DTOs.Order;

namespace Dentizone.Application.Interfaces
{
    public interface IOrderService
    {
        Task<string> CreateOrderAsync(CreateOrderDto createOrderDto, string buyerId);
        Task<OrderViewDto> GetOrderByIdAsync(string orderId, string buyerId);
        Task<List<OrderViewDto>> GetOrdersByBuyerAsync(string buyerId);
        Task<OrderViewDto?> CancelOrderAsync(string orderId);
        Task CompleteOrder(string orderId);
        Task<PagedResultDto<OrderViewDto>> GetOrders(int page, FilterOrderDto filters);

        Task<IEnumerable<Domain.Entity.Order>> GetReviewedOrdersByUserId(string userId);
    }
}