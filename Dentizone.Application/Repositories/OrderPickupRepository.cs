using System.Linq.Expressions;
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

    public async Task<OrderPickup?> FindBy(Expression<Func<OrderPickup, bool>> condition,
        Expression<Func<OrderPickup, object>>[]? includes)
    {
        IQueryable<OrderPickup> query = dbContext.OrderPickups;
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
        dbContext.OrderPickups.Update(entity);
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


    public async Task<OrderPickup?> GetByIdAsync(string id)
    {
        return await dbContext.OrderPickups
            .FirstOrDefaultAsync(o => o.Id == id && !o.IsDeleted);
    }
}