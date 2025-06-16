using System.Linq.Expressions;
using Dentizone.Domain.Entity;

namespace Dentizone.Domain.Interfaces.Repositories;

public interface IOrderRepository : IBaseRepo<Order>
{
    Task<Order> UpdateAsync(Order entity);

    Task<IEnumerable<Order>> GetAllAsync(int page, Expression<Func<Order, bool>> filter,
                                         Expression<Func<Order, object>> orderBy,
                                         Expression<Func<Order, object>>[] includes = null);
}