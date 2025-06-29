using Dentizone.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Dentizone.Domain.Interfaces.Repositories;

namespace Dentizone.Infrastructure.Repositories;

public class OrderStatusRepository(AppDbContext dbContext) : AbstractRepository(dbContext), IOrderStatusRepository
{
    public async Task<OrderStatus?> GetByIdAsync(string id)
    {
        return await DbContext.OrderStatuses
            .FirstOrDefaultAsync(o => o.Id == id);
    }


    public async Task<OrderStatus> CreateAsync(OrderStatus entity)
    {
        await DbContext.OrderStatuses.AddAsync(entity);
        await DbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<OrderStatus?> FindBy(Expression<Func<OrderStatus, bool>> condition,
        Expression<Func<OrderStatus, object>>[]? includes)
    {
        IQueryable<OrderStatus> query = DbContext.OrderStatuses;
        if (includes != null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        return await query.FirstOrDefaultAsync(condition);
    }


    public async Task<IEnumerable<OrderStatus>> Find(Expression<Func<OrderStatus, bool>> filter)
    {
        return await DbContext.OrderStatuses
            .Where(filter)
            .ToListAsync();
    }
}