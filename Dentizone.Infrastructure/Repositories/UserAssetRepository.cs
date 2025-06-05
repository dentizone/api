using Dentizone.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Dentizone.Domain.Interfaces.Repositories;

namespace Dentizone.Infrastructure.Repositories
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


        public async Task<UserAsset> CreateAsync(UserAsset entity)
        {
            await dbContext.UserAssets.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<UserAsset?> FindBy(Expression<Func<UserAsset, bool>> condition,
                                             Expression<Func<UserAsset, object>>[]? includes)
        {
            IQueryable<UserAsset> query = dbContext.UserAssets.Where(u => !u.IsDeleted);
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return await query.FirstOrDefaultAsync(condition);
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

        public async Task<IEnumerable<UserAsset>> GetAllByAsync(int page, Expression<Func<UserAsset, bool>>? condition)
        {
            IQueryable<UserAsset> query = dbContext.UserAssets.Where(u => !u.IsDeleted);
            if (condition != null)
            {
                query = query.Where(condition);
            }

            return await query
                         .OrderByDescending(u => u.CreatedAt)
                         .Skip(CalculatePagination(page))
                         .Take(DefaultPageSize)
                         .ToListAsync();
        }
    }
}