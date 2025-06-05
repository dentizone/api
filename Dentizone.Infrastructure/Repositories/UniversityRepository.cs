using Dentizone.Domain.Entity;
using Dentizone.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Dentizone.Infrastructure.Repositories
{
    internal class UniversityRepository : AbstractRepository, IUniversityRepository
    {
        public UniversityRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<University?> GetByIdAsync(string id)
        {
            return await dbContext.Universities.Where(u => !u.IsDeleted).FirstOrDefaultAsync(u => u.Id == id);
        }


        public async Task<University> CreateAsync(University entity)
        {
            await dbContext.Universities.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<University?> FindBy(Expression<Func<University, bool>> condition,
                                              Expression<Func<University, object>>[]? includes)
        {
            IQueryable<University> query = dbContext.Universities.Where(u => !u.IsDeleted);
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return await query.FirstOrDefaultAsync(condition);
        }

        public async Task<University?> DeleteAsync(string id)
        {
            var toBeDeleted = await GetByIdAsync(id);

            if (toBeDeleted == null)
            {
                return null;
            }

            dbContext.Universities.Remove(toBeDeleted);
            await dbContext.SaveChangesAsync();

            return toBeDeleted;
        }

        public async Task<University> Update(University entity)
        {
            dbContext.Universities.Update(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }


        public async Task<ICollection<University>> GetAll()
        {
            return await dbContext.Universities
                                  .Where(u => !u.IsDeleted)
                                  .ToListAsync();
        }
    }
}