using Dentizone.Domain.Entity;
using Dentizone.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Dentizone.Infrastructure.Repositories
{
    internal class PostAssetRepository(AppDbContext dbContext) : AbstractRepository(dbContext), IPostAssetRepository
    {
        public async Task<PostAsset> CreateAsync(PostAsset entity)
        {
            await DbContext.PostAssets.AddAsync(entity);
            await DbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<PostAsset?> FindBy(Expression<Func<PostAsset, bool>> condition,
            Expression<Func<PostAsset, object>>[]? includes)
        {
            IQueryable<PostAsset> query = DbContext.PostAssets;
            if (includes == null) return await query.FirstOrDefaultAsync(condition);
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(condition);
        }

        public async Task<PostAsset?> DeleteAsync(string id)
        {
            var postAsset = await GetByIdAsync(id);

            DbContext.PostAssets.Remove(postAsset);
            await DbContext.SaveChangesAsync();
            return postAsset;
        }


        public async Task<PostAsset?> GetByIdAsync(string id)
        {
            return await DbContext.PostAssets.FindAsync(id);
        }

        public async Task<PostAsset> UpdateAsync(PostAsset entity)
        {
            DbContext.PostAssets.Update(entity);
            await DbContext.SaveChangesAsync();
            return entity;
        }
    }
}