using Dentizone.Domain.Entity;
using Dentizone.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Dentizone.Infrastructure.Repositories
{
    internal class AssetRepository(AppDbContext dbContext) : AbstractRepository(dbContext), IAssetRepository
    {
        public async Task<Asset> CreateAsync(Asset entity)
        {
            await dbContext.Assets.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<Asset?> FindBy(Expression<Func<Asset, bool>> condition,
                                         Expression<Func<Asset, object>>[]? includes = null)
        {
            var query = dbContext.Assets.AsQueryable();
            if (includes == null)
                return await query
                    .FirstOrDefaultAsync(condition);


            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query
                .FirstOrDefaultAsync(condition);
        }

        public async Task DeleteByIdAsync(string assetId)
        {
            var asset = await GetByIdAsync(assetId);
            if (asset == null)
            {
                return;
            }

            dbContext.Assets.Remove(asset);

            await dbContext.SaveChangesAsync();
        }


        public async Task<Asset?> GetByIdAsync(string id)
        {
            return await dbContext.Assets
                                  .FirstOrDefaultAsync(a => a.Id == id && !a.IsDeleted);
        }

        public async Task<Asset> UpdateAsync(Asset entity)
        {
            dbContext.Assets.Update(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }
    }
}