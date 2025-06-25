using Dentizone.Domain.Entity;
using System.Linq.Expressions;

namespace Dentizone.Domain.Interfaces.Repositories;

public interface IOrderRepository : IBaseRepo<Order>
{
    Task<Order> UpdateAsync(Order entity);
    Task<Order?> GetOrderDetails(string orderId, string buyerId);
    Task<IReadOnlyCollection<Order>> GetOrdersWithDetails(string buyerId);
    Task<PagedResult<Order>> GetAllAsync(int page, Expression<Func<Order, bool>>? filter);

    Task<int> CountTotalOrders();
    Task<decimal> AverageValueOfOrders();
}