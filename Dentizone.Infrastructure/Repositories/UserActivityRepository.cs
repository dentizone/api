using Dentizone.Domain.Entity;
using Dentizone.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Dentizone.Domain.Interfaces;

namespace Dentizone.Infrastructure.Repositories
{
    public class UserActivityRepository(AppDbContext dbContext) : AbstractRepository(dbContext), IUserActivityRepository
    {
        public async Task<UserActivity?> GetByIdAsync(string id)
        {
            return await DbContext.UserActivities
                .FirstOrDefaultAsync(u => u.Id == id);
        }


        public async Task<UserActivity> CreateAsync(UserActivity entity)
        {
            await DbContext.UserActivities.AddAsync(entity);
            await DbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<UserActivity?> FindBy(Expression<Func<UserActivity, bool>> condition,
            Expression<Func<UserActivity, object>>[]? includes)
        {
            IQueryable<UserActivity> query = DbContext.UserActivities;
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return await query.FirstOrDefaultAsync(condition);
        }

        public async Task<ICollection<UserActivity>> GetAllBy(int page, Expression<Func<UserActivity, bool>>? filter)
        {
            IQueryable<UserActivity> query = DbContext.UserActivities;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query
                .OrderByDescending(u => u.CreatedAt)
                .Skip(CalculatePagination(page))
                .Take(DefaultPageSize)
                .ToListAsync();
        }

        public async Task<PagedResult<UserActivity>> GetAllAsync(int page,
            Expression<Func<UserActivity, bool>>? filters)
        {
            var q = DbContext.UserActivities.AsQueryable();
            var totalCount = await q.CountAsync();

            var pagedQuery = await BuildPagedQuery(page, filters, q);

            var included = pagedQuery.Query
                .Include(u => u.User);
            var items = await included.ToListAsync();
            return new PagedResult<UserActivity>
            {
                Items = items,
                TotalCount = totalCount,
                PageSize = DefaultPageSize,
                Page = page
            };
        }
    }
}