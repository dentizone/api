using Dentizone.Application.Abstracts;
using Dentizone.Application.Interfaces;
using Dentizone.Domain.Entity;
using Dentizone.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Dentizone.Application.Repositories
{
    internal class CategoryRepository : AbstractRepository, ICategoryRepository
    {
        private AppDbContext DbContext { set; get; }

        public CategoryRepository(AppDbContext dbContext) : base(dbContext)
        {
            DbContext = dbContext;
        }

        public async Task<Category> CreateAsync(Category entity)
        {
            await DbContext.Categories.AddAsync(entity);
            await DbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<Category> DeleteAsync(string id)
        {
            var deletedCategory =
                await DbContext.Categories.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

            DbContext.Categories.Remove(deletedCategory);
            await DbContext.SaveChangesAsync();
            return deletedCategory;
        }

        public async Task<IEnumerable<Category>> GetAllAsync(int page = 1)
        {
            var categories = await DbContext.Categories.Where(c => !c.IsDeleted)
                                            .Skip(CalculatePagination(page))
                                            .Take(DefaultPageSize)
                                            .ToListAsync();
            return categories;
        }

        public async Task<Category> GetByIdAsync(string id)
        {
            var category = await DbContext.Categories.Where(c => c.Id == id && !c.IsDeleted)
                                          .FirstOrDefaultAsync();
            return category;
        }

        public async Task<Category?> UpdateAsync(Category entity)
        {
            var existingCategory =
                await DbContext.Categories.FirstOrDefaultAsync(c => c.Id == entity.Id && !c.IsDeleted);

            if (existingCategory is null) return null;

            existingCategory.Name = entity.Name;
            DbContext.Categories.Update(existingCategory);

            await DbContext.SaveChangesAsync();

            return entity;
        }
    }
}