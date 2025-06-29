using Dentizone.Domain.Entity;
using Dentizone.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Dentizone.Infrastructure.Repositories
{
    internal class UserAssetRepository(AppDbContext dbContext) : AbstractRepository(dbContext), IUserAssetRepository
    {
        public async Task<UserAsset?> GetByIdAsync(string id)
        {
            return await DbContext.UserAssets
                .FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted);
        }


        public async Task<UserAsset> CreateAsync(UserAsset entity)
        {
            await DbContext.UserAssets.AddAsync(entity);
            await DbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<UserAsset?> FindBy(Expression<Func<UserAsset, bool>> condition,
            Expression<Func<UserAsset, object>>[]? includes)
        {
            IQueryable<UserAsset> query = DbContext.UserAssets.Where(u => !u.IsDeleted);
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

            DbContext.UserAssets.Remove(toBeDeleted);
            await DbContext.SaveChangesAsync();
            return toBeDeleted;
        }

        public async Task<IEnumerable<UserAsset>> GetAllByAsync(int page, Expression<Func<UserAsset, bool>>? condition)
        {
            IQueryable<UserAsset> query = DbContext.UserAssets.Where(u => !u.IsDeleted);
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