using System.Linq.Expressions;
using Dentizone.Application.Abstracts;
using Dentizone.Application.Interfaces;
using Dentizone.Domain.Entity;
using Dentizone.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Dentizone.Application.Repositories
{
    internal class ShipmentActivityRepository : AbstractRepository, IShipmentActivityRepository
    {
        public ShipmentActivityRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<ShipmentActivity?> GetByIdAsync(string id)
        {
            return await
                dbContext.ShipmentActivities
                    .FirstOrDefaultAsync(s => s.Id == id);
        }


        public async Task<ShipmentActivity> CreateAsync(ShipmentActivity entity)
        {
            await dbContext.ShipmentActivities.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<ShipmentActivity?> FindBy(Expression<Func<ShipmentActivity, bool>> condition,
            Expression<Func<ShipmentActivity, object>>[]? includes)
        {
            IQueryable<ShipmentActivity> query = dbContext.ShipmentActivities;
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

            dbContext.ShipmentActivities.Remove(toBeDeleted);
            await dbContext.SaveChangesAsync();
            return toBeDeleted;
        }
    }
}