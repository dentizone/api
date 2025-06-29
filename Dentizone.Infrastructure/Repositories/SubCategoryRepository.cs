using Dentizone.Domain.Entity;
using Dentizone.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Dentizone.Infrastructure.Repositories
{
    internal class SubCategoryRepository(AppDbContext dbContext) : AbstractRepository(dbContext),
        ISubCategoryRepository
    {
        public async Task<SubCategory?> GetByIdAsync(string id)
        {
            return await DbContext.SubCategories
                .FirstOrDefaultAsync(sc => sc.Id == id);
        }


        public async Task<SubCategory> CreateAsync(SubCategory entity)
        {
            await DbContext.SubCategories.AddAsync(entity);
            await DbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<SubCategory?> FindBy(Expression<Func<SubCategory, bool>> condition,
            Expression<Func<SubCategory, object>>[]? includes)
        {
            IQueryable<SubCategory> query = DbContext.SubCategories;
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return await query.FirstOrDefaultAsync(condition);
        }

        public async Task<SubCategory?> DeleteAsync(string id)
        {
            var toBeDeleted = await GetByIdAsync(id);

            if (toBeDeleted == null)
            {
                return null;
            }

            DbContext.SubCategories.Remove(toBeDeleted);
            await DbContext.SaveChangesAsync();

            return toBeDeleted;
        }

        public async Task<ICollection<SubCategory>> GetAll()
        {
            return await DbContext.SubCategories
                .Include(sc => sc.Category)
                .ToListAsync();
        }

        public async Task<SubCategory?> Update(SubCategory entity)
        {
            DbContext.SubCategories.Update(entity);
            await DbContext.SaveChangesAsync();

            return entity;
        }
    }
}