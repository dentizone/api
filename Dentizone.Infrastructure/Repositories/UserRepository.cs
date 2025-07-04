using System.Linq.Expressions;
using Dentizone.Domain.Entity;
using Dentizone.Domain.Interfaces;
using Dentizone.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Dentizone.Infrastructure.Repositories
{
    internal class UserRepository(AppDbContext dbContext) : AbstractRepository(dbContext), IUserRepository
    {
        public async Task<AppUser?> GetByIdAsync(string id)
        {
            return await
                DbContext.AppUsers
                    .FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted);
        }

        public async Task<PagedResult<AppUser>> GetAllAsync(int page = 1,
            Expression<Func<AppUser, bool>>? filter = null)
        {
            var query = DbContext.AppUsers.AsQueryable();

            var pagedQuery = await BuildPagedQuery(page, filter, query);
            query = pagedQuery.Query;
            var totalCount = pagedQuery.TotalCount;

            query = query.Include(u => u.University);

            query = query.OrderByDescending(u => u.CreatedAt);

            return new PagedResult<AppUser>
            {
                Items = await query.AsNoTracking().AsSplitQuery().ToListAsync(),
                Page = page,
                PageSize = DefaultPageSize,
                TotalCount = totalCount
            };
        }

        public async Task<AppUser> CreateAsync(AppUser entity)
        {
            await DbContext.AppUsers.AddAsync(entity);
            await DbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<AppUser?> FindBy(Expression<Func<AppUser, bool>> condition,
            Expression<Func<AppUser, object>>[]? includes)
        {
            IQueryable<AppUser> query = DbContext.AppUsers.Where(u => !u.IsDeleted);
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return await query.FirstOrDefaultAsync(condition);
        }

        public async Task<AppUser?> DeleteAsync(string id)
        {
            var toBeDeleted = await GetByIdAsync(id);
            if (toBeDeleted == null)
            {
                return null;
            }

            DbContext.AppUsers.Remove(toBeDeleted);
            await DbContext.SaveChangesAsync();
            return toBeDeleted;
        }

        public async Task<AppUser> Update(AppUser entity)
        {
            DbContext.AppUsers.Update(entity);
            await DbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<int> GetCountOfUsersAsync()
        {
            var count = await DbContext.AppUsers.Where(u => !u.IsDeleted).CountAsync();
            return count;
        }

        public async Task<int> GetCount7DaysAsync()
        {
            var count = await DbContext.AppUsers.Where(u => !u.IsDeleted && u.CreatedAt >= DateTime.UtcNow.AddDays(-7))
                .CountAsync();
            return count;
        }

        public async Task<int> GetCount30DaysAsync()
        {
            var count = await DbContext.AppUsers.Where(u => !u.IsDeleted && u.CreatedAt >= DateTime.UtcNow.AddDays(-30))
                .CountAsync();
            return count;
        }

        public async Task<Dictionary<string, int>> GetStudentCountPerUniversityAsync()
        {
            var result = await DbContext.AppUsers
                .Where(a => !a.IsDeleted && a.University != null)
                .GroupBy(a => a.University.Name)
                .ToDictionaryAsync(g => g.Key, g => g.Count());

            return result;
        }

        public async Task<Dictionary<string, int>> GetUsersPerStateAsync()
        {
            var result = await DbContext.AppUsers
                .GroupBy(a => a.Status)
                .ToDictionaryAsync(g => g.Key.ToString(), g => g.Count());
            return result;
        }
    }
}