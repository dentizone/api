using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Dentizone.Application.Abstracts;
using Dentizone.Application.Interfaces;
using Dentizone.Domain.Entity;
using Dentizone.Infrastructure;

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

        public async Task<PostAsset?> DeleteAsync(int id)
        {
            var postAsset = await GetByIdAsync(id);
            if (postAsset == null)
            {
                return null;
            }
            dbContext.PostAssets.Remove(postAsset);
            await dbContext.SaveChangesAsync();
            return postAsset;
        }

        public async Task<IEnumerable<PostAsset>> GetAllAsync(int page = 1)
        {
            int skippedPages  = CalculatePagination(page);
            return await dbContext.PostAssets
                            .Skip(skippedPages)
                            .Take(DefaultPageSize)
                            .ToListAsync();
        }

        public async Task<PostAsset?> GetByIdAsync(int id)
        {
            return await dbContext.PostAssets.FindAsync(id);
        }

        public async Task<PostAsset> UpdateAsync(PostAsset entity)
        {
            var isExists = await dbContext.PostAssets.FindAsync(entity.Id);
            if (isExists == null)
                return null;

            if(entity.DisplayOrder != 0)
            {
                isExists.DisplayOrder = entity.DisplayOrder;
            }

            isExists.UpdatedAt = DateTime.UtcNow;

            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return isExists;
        }

    }
}
}
