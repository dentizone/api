using System.Linq.Expressions;
using Dentizone.Application.Abstracts;
using Dentizone.Application.Interfaces;
using Dentizone.Domain.Entity;
using Dentizone.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Dentizone.Application.Repositories;

public class OrderStatusRepository : AbstractRepository, IOrderStatusRepository
{
    public OrderStatusRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<OrderStatus?> GetByIdAsync(string id)
    {
        return await dbContext.OrderStatuses
            .FirstOrDefaultAsync(o => o.Id == id);
    }


    public async Task<OrderStatus> CreateAsync(OrderStatus entity)
    {
        await dbContext.OrderStatuses.AddAsync(entity);
        await dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<OrderStatus?> FindBy(Expression<Func<OrderStatus, bool>> condition,
        Expression<Func<OrderStatus, object>>[]? includes)
    {
        IQueryable<OrderStatus> query = dbContext.OrderStatuses;
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
        return await dbContext.OrderStatuses
            .Where(filter)
            .ToListAsync();
    }
}