using Dentizone.Application.Abstracts;
using Dentizone.Application.Interfaces;
using Dentizone.Domain.Entity;
using Dentizone.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Dentizone.Application.Repositories
{
    internal class AssetRepository : AbstractRepository, IAssetRepository
    {
        public AssetRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Asset> CreateAsync(Asset entity)
        {
            await dbContext.Assets.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<Asset?> DeleteAsync(string id)
        {
            var asset = await GetByIdAsync(id);
            dbContext.Assets.Remove(asset);
            await dbContext.SaveChangesAsync();
            return asset;
        }

        public async Task<IEnumerable<Asset>> GetAllAsync(int page = 1)
        {
            var skippedPages = CalculatePagination(page);
            return await dbContext.Assets
                                  .Skip(skippedPages)
                                  .Take(DefaultPageSize)
                                  .ToListAsync();
        }

        public async Task<Asset?> GetByIdAsync(string id)
        {
            return await dbContext.Assets.FindAsync(id);
        }

        public async Task<Asset> UpdateAsync(Asset entity)
        {
            dbContext.Update(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }
    }
}