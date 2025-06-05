using System.Linq.Expressions;
using Dentizone.Application.Abstracts;
using Dentizone.Application.Interfaces;
using Dentizone.Domain.Entity;
using Dentizone.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Dentizone.Application.Repositories
{
    internal class UserRepository : AbstractRepository, IUserRepository
    {
        public UserRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<AppUser?> GetByIdAsync(string id)
        {
            return await
                dbContext.AppUsers
                    .FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted);
        }

        public async Task<IEnumerable<AppUser>> GetAllAsync(int page = 1)
        {
            return
                await
                    dbContext.AppUsers
                        .Skip(CalculatePagination(page))
                        .Take(DefaultPageSize)
                        .ToListAsync();
        }

        public async Task<AppUser> CreateAsync(AppUser entity)
        {
            await dbContext.AppUsers.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<AppUser?> FindBy(Expression<Func<AppUser, bool>> condition,
            Expression<Func<AppUser, object>>[]? includes)
        {
            IQueryable<AppUser> query = dbContext.AppUsers.Where(u => !u.IsDeleted);
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

            dbContext.AppUsers.Remove(toBeDeleted);
            await dbContext.SaveChangesAsync();
            return toBeDeleted;
        }

        public async Task<AppUser> Update(AppUser entity)
        {
            dbContext.AppUsers.Update(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }
    }
}