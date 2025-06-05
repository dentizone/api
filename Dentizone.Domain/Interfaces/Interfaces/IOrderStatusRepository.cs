using System.Linq.Expressions;
using Dentizone.Application.Interfaces;
using Dentizone.Domain.Entity;

namespace Dentizone.Application.Interfaces;

interface IOrderStatusRepository : IBaseRepo<OrderStatus>
{
    Task<IEnumerable<OrderStatus>> Find(Expression<Func<OrderStatus, bool>> filter);
}