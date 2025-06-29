using Dentizone.Domain.Entity;
using Dentizone.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Dentizone.Infrastructure.Repositories
{
    internal class ShipmentActivityRepository(AppDbContext dbContext)
        : AbstractRepository(dbContext), IShipmentActivityRepository
    {
        public async Task<ShipmentActivity?> GetByIdAsync(string id)
        {
            return await
                DbContext.ShipmentActivities
                    .FirstOrDefaultAsync(s => s.Id == id);
        }


        public async Task<ShipmentActivity> CreateAsync(ShipmentActivity entity)
        {
            await DbContext.ShipmentActivities.AddAsync(entity);
            await DbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<ShipmentActivity?> FindBy(Expression<Func<ShipmentActivity, bool>> condition,
            Expression<Func<ShipmentActivity, object>>[]? includes)
        {
            IQueryable<ShipmentActivity> query = DbContext.ShipmentActivities;
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return await query.FirstOrDefaultAsync(condition);
        }

        public async Task<ShipmentActivity?> DeleteAsync(string id)
        {
            var toBeDeleted = await GetByIdAsync(id);

            if (toBeDeleted == null)
            {
                return null;
            }

            DbContext.ShipmentActivities.Remove(toBeDeleted);
            await DbContext.SaveChangesAsync();
            return toBeDeleted;
        }
    }
}