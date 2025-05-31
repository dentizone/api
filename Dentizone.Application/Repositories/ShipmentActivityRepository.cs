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
                dbContext.ShipmentActivites
                         .FirstOrDefaultAsync(s => s.Id == id && !s.IsDeleted);
        }

        public async Task<IEnumerable<ShipmentActivity>> GetAllAsync(int page = 1)
        {
            return await dbContext.ShipmentActivites
                                  .Where(s => !s.IsDeleted)
                                  .Skip(CalculatePagination(page))
                                  .Take(DefaultPageSize)
                                  .ToListAsync();
        }

        public async Task<ShipmentActivity> CreateAsync(ShipmentActivity entity)
        {
            await dbContext.ShipmentActivites.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<ShipmentActivity?> DeleteAsync(string id)
        {
            var toBeDeleted = await GetByIdAsync(id);

            dbContext.ShipmentActivites.Remove(toBeDeleted);
            await dbContext.SaveChangesAsync();
            return toBeDeleted;
        }
    }
}