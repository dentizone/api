using Dentizone.Application.Abstracts;
using Dentizone.Application.Interfaces;
using Dentizone.Domain.Entity;
using Dentizone.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Dentizone.Application.Repositories
{
    internal class ShipInfoRepository : AbstractRepository, IShipInfoRepository
    {
        public ShipInfoRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<ShipInfo?> GetByIdAsync(string id)
        {
            return await dbContext.ShipInfos
                                  .FirstOrDefaultAsync(s => s.Id == id && !s.IsDeleted);
        }

        public async Task<IEnumerable<ShipInfo>> GetAllAsync(int page = 1)
        {
            return await dbContext.ShipInfos
                                  .Where(s => !s.IsDeleted)
                                  .Skip(CalculatePagination(page))
                                  .Take(DefaultPageSize)
                                  .ToListAsync();
        }

        public async Task<ShipInfo> CreateAsync(ShipInfo entity)
        {
            await dbContext.ShipInfos.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<ShipInfo?> DeleteAsync(string id)
        {
            var entity = await GetByIdAsync(id);
            dbContext.ShipInfos.Remove(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<ShipInfo> Update(ShipInfo entity)
        {
            dbContext.ShipInfos.Update(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }
    }
}