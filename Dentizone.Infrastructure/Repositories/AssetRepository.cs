using Dentizone.Application.Abstracts;
using Dentizone.Application.Interfaces;
using Dentizone.Domain.Entity;
using Dentizone.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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

        public async Task<Asset?> FindBy(Expression<Func<Asset, bool>> condition,
                                         Expression<Func<Asset, object>>[]? includes)
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

        public async Task<Asset?> DeleteAsync(string id)
        {
            var asset = await GetByIdAsync(id);
            dbContext.Assets.Remove(asset);
            await dbContext.SaveChangesAsync();
            return asset;
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