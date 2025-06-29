using Dentizone.Domain.Entity;
using Dentizone.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Dentizone.Infrastructure.Repositories;

public class OrderPickupRepository(AppDbContext dbContext) : AbstractRepository(dbContext), IOrderPickupRepository
{
    public async Task<OrderPickup> CreateAsync(OrderPickup entity)
    {
        await DbContext.OrderPickups.AddAsync(entity);
        await DbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<OrderPickup?> FindBy(Expression<Func<OrderPickup, bool>> condition,
        Expression<Func<OrderPickup, object>>[]? includes)
    {
        IQueryable<OrderPickup> query = DbContext.OrderPickups;
        if (includes != null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        return await query
            .FirstOrDefaultAsync(condition);
    }


    public async Task<OrderPickup> UpdateAsync(OrderPickup entity)
    {
        DbContext.OrderPickups.Update(entity);
        await DbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<OrderPickup?> DeleteAsync(string id)
    {
        var toBeDeleted = await GetByIdAsync(id);
        if (toBeDeleted == null)
        {
            return null;
        }

        DbContext.OrderPickups.Remove(toBeDeleted);
        await DbContext.SaveChangesAsync();
        return toBeDeleted;
    }


    public async Task<OrderPickup?> GetByIdAsync(string id)
    {
        return await DbContext.OrderPickups
            .FirstOrDefaultAsync(o => o.Id == id && !o.IsDeleted);
    }
}