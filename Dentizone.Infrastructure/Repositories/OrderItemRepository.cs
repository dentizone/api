using System.Linq.Expressions;
using Dentizone.Application.Abstracts;
using Dentizone.Application.Interfaces;
using Dentizone.Domain.Entity;
using Dentizone.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Dentizone.Application.Repositories;

public class OrderItemRepository : AbstractRepository, IOrderItemRepository
{
    public OrderItemRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<OrderItem?> GetByIdAsync(string id)
    {
        return
            await dbContext.OrderItems
                .FirstOrDefaultAsync(o => o.Id == id);
    }


    public async Task<OrderItem> CreateAsync(OrderItem entity)
    {
        await dbContext.OrderItems.AddAsync(entity);
        await dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<OrderItem?> FindBy(Expression<Func<OrderItem, bool>> condition,
        Expression<Func<OrderItem, object>>[]? includes)
    {
        IQueryable<OrderItem> query = dbContext.OrderItems;
        if (includes != null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        return await query.FirstOrDefaultAsync(condition);
    }
}