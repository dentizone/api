using Dentizone.Domain.Entity;
using System.Linq.Expressions;

namespace Dentizone.Domain.Interfaces.Repositories;

public interface IOrderStatusRepository : IBaseRepo<OrderStatus>
{
    Task<IEnumerable<OrderStatus>> Find(Expression<Func<OrderStatus, bool>> filter);
}