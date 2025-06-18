using Dentizone.Domain.Entity;
using Dentizone.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Dentizone.Infrastructure.Repositories
{
    public class CategoryRepository(AppDbContext dbContext) : AbstractRepository(dbContext), ICategoryRepository
    {
        private readonly AppDbContext DbContext = dbContext;

        public async Task<Category> CreateAsync(Category entity)
        {
            await DbContext.Categories.AddAsync(entity);
            await DbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<Category?> FindBy(Expression<Func<Category, bool>> condition
                                            , Expression<Func<Category, object>>[]? includes)
        {
            IQueryable<Category> query = DbContext.Categories;
            if (includes == null) return await query.FirstOrDefaultAsync(condition);
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(condition);
        }


        public async Task<Category?> GetByIdAsync(string id)
        {
            var category = await DbContext.Categories.Where(c => c.Id == id && !c.IsDeleted)
                                          .FirstOrDefaultAsync();
            return category;
        }

        public async Task<Category?> Update(Category entity)
        {
            DbContext.Categories.Update(entity);
            await DbContext.SaveChangesAsync();

            return entity;
        }

        public async Task<Category?> Delete(string id)
        {
            var category = await GetByIdAsync(id);
            if (category == null)
            {
                return null;
            }

            DbContext.Categories.Remove(category);
            await DbContext.SaveChangesAsync();
            return category;
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            return await DbContext.Categories
                                  .Where(c => !c.IsDeleted)
                                  .ToListAsync();
        }
    }
}