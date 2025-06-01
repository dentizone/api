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
            .FirstOrDefaultAsync(o => o.Id == id && !o.IsDeleted);
    }

    public async Task<IEnumerable<OrderStatus>> GetAllAsync(int page = 1)
    {
        return await dbContext.OrderStatuses
            .Where(o => !o.IsDeleted)
            .Skip(CalculatePagination(page))
            .Take(DefaultPageSize)
            .ToListAsync();
    }

    public async Task<OrderStatus> CreateAsync(OrderStatus entity)
    {
        await dbContext.OrderStatuses.AddAsync(entity);
        await dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<OrderStatus?> DeleteAsync(string id)
    {
        var toBeDeleted = await GetByIdAsync(id);
        if (toBeDeleted == null)
        {
            return null;
        }

        dbContext.OrderStatuses.Remove(toBeDeleted);
        await dbContext.SaveChangesAsync();
        return toBeDeleted;
    }
}