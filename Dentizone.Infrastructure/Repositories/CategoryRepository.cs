using Dentizone.Domain.Entity;
using Dentizone.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Dentizone.Infrastructure.Repositories
{
    public class CategoryRepository(AppDbContext dbContext) : AbstractRepository(dbContext), ICategoryRepository
    {
        private readonly AppDbContext _dbContext = dbContext;

        public async Task<Category> CreateAsync(Category entity)
        {
            await _dbContext.Categories.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<Category?> FindBy(Expression<Func<Category, bool>> condition
            , Expression<Func<Category, object>>[]? includes)
        {
            IQueryable<Category> query = _dbContext.Categories;
            if (includes == null) return await query.FirstOrDefaultAsync(condition);
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(condition);
        }


        public async Task<Category?> GetByIdAsync(string id)
        {
            var category = await _dbContext.Categories.Where(c => c.Id == id && !c.IsDeleted)
                .FirstOrDefaultAsync();
            return category;
        }

        public async Task<Category?> Update(Category entity)
        {
            _dbContext.Categories.Update(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task<Category?> Delete(string id)
        {
            var category = await GetByIdAsync(id);
            if (category == null)
            {
                return null;
            }

            _dbContext.Categories.Remove(category);
            await _dbContext.SaveChangesAsync();
            return category;
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            return await _dbContext.Categories
                .Where(c => !c.IsDeleted)
                .ToListAsync();
        }
    }
}