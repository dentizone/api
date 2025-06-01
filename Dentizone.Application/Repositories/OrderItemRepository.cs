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
                           .FirstOrDefaultAsync(o => o.Id == id && !o.IsDeleted);
    }

    public async Task<IEnumerable<OrderItem>> GetAllAsync(int page = 1)
    {
        return
            await dbContext.OrderItems
                           .Where(o => !o.IsDeleted)
                           .Skip(CalculatePagination(page))
                           .Take(DefaultPageSize)
                           .ToListAsync();
    }

    public async Task<OrderItem> CreateAsync(OrderItem entity)
    {
        await dbContext.OrderItems.AddAsync(entity);
        await dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<OrderItem?> DeleteAsync(string id)
    {
        var toBeDeleted = await GetByIdAsync(id);
        if (toBeDeleted == null)
        {
            return null;
        }

        dbContext.OrderItems.Remove(toBeDeleted);
        await dbContext.SaveChangesAsync();
        return toBeDeleted;
    }
}