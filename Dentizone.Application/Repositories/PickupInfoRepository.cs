using Dentizone.Application.Abstracts;
using Dentizone.Application.Interfaces;
using Dentizone.Domain.Entity;
using Dentizone.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Dentizone.Application.Repositories
{
    internal class PickupInfoRepository : AbstractRepository, IPickupInfoRepository
    {
        public PickupInfoRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<PickupInfo?> GetByIdAsync(string id)
        {
            return await dbContext.PickupInfos
                                  .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);
        }

        public async Task<IEnumerable<PickupInfo>> GetAllAsync(int page = 1)
        {
            return await dbContext.PickupInfos
                                  .Where(p => !p.IsDeleted)
                                  .Skip(CalculatePagination(page))
                                  .Take(DefaultPageSize)
                                  .ToListAsync();
        }

        public async Task<PickupInfo> CreateAsync(PickupInfo entity)
        {
            await dbContext.PickupInfos.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<PickupInfo?> DeleteAsync(string id)
        {
            var toBeDeleted = await GetByIdAsync(id);
            dbContext.PickupInfos.Remove(toBeDeleted);
            await dbContext.SaveChangesAsync();
            return toBeDeleted;
        }

        public async Task<PickupInfo> Update(PickupInfo entity)
        {
            dbContext.PickupInfos.Update(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }
    }
}