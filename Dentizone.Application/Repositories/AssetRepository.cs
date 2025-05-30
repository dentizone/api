using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dentizone.Application.Abstracts;
using Dentizone.Application.Interfaces;
using Dentizone.Domain.Entity;
using Dentizone.Domain.Enums;
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

        public async Task<Asset> DeleteAsync(int id)
        {
            var asset = await GetByIdAsync(id);
            if (asset == null)
            {
                return null;
            }
            dbContext.Assets.Remove(asset);
            await dbContext.SaveChangesAsync();
            return asset;
        }

        public async Task<IEnumerable<Asset>> GetAllAsync(int page = 1)
        {
            int skippedPages = CalculatePagination(page);
            return await dbContext.Assets
                            .Skip(skippedPages)
                            .Take(DefaultPageSize)
                            .ToListAsync();
        }

        public async Task<Asset> GetByIdAsync(int id)
        {
            return await dbContext.Assets.FindAsync(id);
        }

        public async Task<Asset> UpdateAsync(Asset entity)
        {
            var isExists = await dbContext.Assets.FindAsync(entity.Id);
            if (isExists == null)
                return null;

            if (!string.IsNullOrEmpty(entity.Url))
                isExists.Url = entity.Url;

            if (entity.Size != 0)
                isExists.Size = entity.Size;

            if (Enum.IsDefined(typeof(AssetType), entity.Type))
            {
                if (entity.Type != entity.Type)
                    isExists.Type = entity.Type;
            }
            if (Enum.IsDefined(typeof(AssetStatus), entity.Status))
            {
                if (entity.Status != entity.Status)
                    isExists.Status = entity.Status;
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
