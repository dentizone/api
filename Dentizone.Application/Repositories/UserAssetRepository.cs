using Dentizone.Application.Abstracts;
using Dentizone.Application.Interfaces;
using Dentizone.Domain.Entity;
using Dentizone.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Dentizone.Application.Repositories
{
    internal class UserAssetRepository : AbstractRepository, IUserAssetRepository
    {
        public UserAssetRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<UserAsset?> GetByIdAsync(string id)
        {
            return await dbContext.UserAssets
                                  .FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted);
        }

        public async Task<IEnumerable<UserAsset>> GetAllAsync(int page = 1)
        {
            return await
                dbContext.UserAssets
                         .Where(u => !u.IsDeleted)
                         .Skip(CalculatePagination(page))
                         .Take(DefaultPageSize)
                         .ToListAsync();
        }

        public async Task<UserAsset> CreateAsync(UserAsset entity)
        {
            await dbContext.UserAssets.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<UserAsset?> DeleteAsync(string id)
        {
            var toBeDeleted = await GetByIdAsync(id);
            if (toBeDeleted == null)
            {
                return null;
            }

            dbContext.UserAssets.Remove(toBeDeleted);
            await dbContext.SaveChangesAsync();
            return toBeDeleted;
        }
    }
}