using Dentizone.Domain.Entity;
using Dentizone.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Dentizone.Infrastructure.Repositories
{
    public class UserActivityRepository(AppDbContext dbContext) : AbstractRepository(dbContext), IUserActivityRepository
    {
        public async Task<UserActivity?> GetByIdAsync(string id)
        {
            return await dbContext.UserActivities
                .FirstOrDefaultAsync(u => u.Id == id);
        }


        public async Task<UserActivity> CreateAsync(UserActivity entity)
        {
            await dbContext.UserActivities.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<UserActivity?> FindBy(Expression<Func<UserActivity, bool>> condition,
            Expression<Func<UserActivity, object>>[]? includes)
        {
            IQueryable<UserActivity> query = dbContext.UserActivities;
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
            IQueryable<UserActivity> query = dbContext.UserActivities;
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
    }
}