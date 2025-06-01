using Dentizone.Application.Abstracts;
using Dentizone.Application.Interfaces;
using Dentizone.Domain.Entity;
using Dentizone.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Dentizone.Application.Repositories;

public class OrderPickupRepository : AbstractRepository, IOrderPickupRepository
{
    public OrderPickupRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<OrderPickup> CreateAsync(OrderPickup entity)
    {
        await dbContext.OrderPickups.AddAsync(entity);
        await dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<OrderPickup?> DeleteAsync(string id)
    {
        var toBeDeleted = await GetByIdAsync(id);
        if (toBeDeleted == null)
        {
            return null;
        }

        dbContext.OrderPickups.Remove(toBeDeleted);
        await dbContext.SaveChangesAsync();
        return toBeDeleted;
    }

    public async Task<IEnumerable<OrderPickup>> GetAllAsync(int page = 1)
    {
        return await dbContext.OrderPickups
                              .Where(o => !o.IsDeleted)
                              .Skip(CalculatePagination(page))
                              .Take(DefaultPageSize)
                              .ToListAsync();
    }

    public async Task<OrderPickup?> GetByIdAsync(string id)
    {
        return await dbContext.OrderPickups
                              .FirstOrDefaultAsync(o => o.Id == id && !o.IsDeleted);
    }
}