using System.Linq.Expressions;
using Dentizone.Application.Abstracts;
using Dentizone.Application.Interfaces;
using Dentizone.Domain.Entity;
using Dentizone.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Dentizone.Application.Repositories
{
    internal class SubCategoryRepository : AbstractRepository,
        ISubCategoryRepository
    {
        public SubCategoryRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<SubCategory?> GetByIdAsync(string id)
        {
            return await dbContext.SubCategories
                .FirstOrDefaultAsync(sc => sc.Id == id);
        }


        public async Task<SubCategory> CreateAsync(SubCategory entity)
        {
            await dbContext.SubCategories.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<SubCategory?> FindBy(Expression<Func<SubCategory, bool>> condition,
            Expression<Func<SubCategory, object>>[]? includes)
        {
            IQueryable<SubCategory> query = dbContext.SubCategories;
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

            dbContext.SubCategories.Remove(toBeDeleted);

            return toBeDeleted;
        }

        public async Task<ICollection<SubCategory>> GetAll()
        {
            return await dbContext.SubCategories
                .Include(sc => sc.Category)
                .ToListAsync();
        }

        public async Task<SubCategory?> Update(SubCategory entity)
        {
            dbContext.SubCategories.Update(entity);
            await dbContext.SaveChangesAsync();

            return entity;
        }
    }
}