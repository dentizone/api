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
            await DbContext.Assets.AddAsync(entity);
            await DbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<Asset?> FindBy(Expression<Func<Asset, bool>> condition,
            Expression<Func<Asset, object>>[]? includes = null)
        {
            var query = DbContext.Assets.AsQueryable();
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

            DbContext.Assets.Remove(asset);

            await DbContext.SaveChangesAsync();
        }


        public async Task<Asset?> GetByIdAsync(string id)
        {
            return await DbContext.Assets
                .FirstOrDefaultAsync(a => a.Id == id && !a.IsDeleted);
        }

        public async Task<Asset> UpdateAsync(Asset entity)
        {
            DbContext.Assets.Update(entity);
            await DbContext.SaveChangesAsync();
            return entity;
        }
    }
}