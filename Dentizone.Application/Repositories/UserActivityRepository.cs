using Dentizone.Application.Abstracts;
using Dentizone.Application.Interfaces;
using Dentizone.Domain.Entity;
using Dentizone.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Dentizone.Application.Repositories
{
    internal class UserActivityRepository : AbstractRepository, IUserActivityRepository
    {
        public UserActivityRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<UserActivity?> GetByIdAsync(string id)
        {
            return await dbContext.UserActivities
                                  .FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted);
        }

        public async Task<IEnumerable<UserActivity>> GetAllAsync(int page = 1)
        {
            return await dbContext.UserActivities
                                  .Where(u => !u.IsDeleted)
                                  .Skip(CalculatePagination(page))
                                  .Take(DefaultPageSize)
                                  .ToListAsync();
        }

        public async Task<UserActivity> CreateAsync(UserActivity entity)
        {
            await dbContext.UserActivities.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<UserActivity?> DeleteAsync(string id)
        {
            var toBeDeleted = await GetByIdAsync(id);
            if (toBeDeleted == null)
            {
                return null;
            }

            dbContext.UserActivities.Remove(toBeDeleted);
            await dbContext.SaveChangesAsync();
            return toBeDeleted;
        }
    }
}