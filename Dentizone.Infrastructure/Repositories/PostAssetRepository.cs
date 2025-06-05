using System.Linq.Expressions;
using Dentizone.Application.Abstracts;
using Dentizone.Application.Interfaces;
using Dentizone.Domain.Entity;
using Dentizone.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Dentizone.Application.Repositories
{
    internal class PostAssetRepository : AbstractRepository, IPostAssetRepository
    {
        public PostAssetRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<PostAsset> CreateAsync(PostAsset entity)
        {
            await dbContext.PostAssets.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<PostAsset?> FindBy(Expression<Func<PostAsset, bool>> condition,
            Expression<Func<PostAsset, object>>[]? includes)
        {
            IQueryable<PostAsset> query = dbContext.PostAssets;
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

            dbContext.PostAssets.Remove(postAsset);
            await dbContext.SaveChangesAsync();
            return postAsset;
        }


        public async Task<PostAsset?> GetByIdAsync(string id)
        {
            return await dbContext.PostAssets.FindAsync(id);
        }

        public async Task<PostAsset> UpdateAsync(PostAsset entity)
        {
            dbContext.PostAssets.Update(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }
    }
}