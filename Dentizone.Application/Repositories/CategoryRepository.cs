using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dentizone.Application.Abstracts;
using Dentizone.Application.Interfaces;
using Dentizone.Domain.Entity;
using Dentizone.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Dentizone.Application.Repositories
{
    //MMMMMMMM
    internal class CategoryRepository : AbstractRepository, IcategoryRepository
    {
        private AppDbContext DbContext { set; get; }
        public CategoryRepository(AppDbContext dbContext) : base(dbContext)
        {
            DbContext = dbContext;
        }
        public async Task<Category> CreateAsync(Category entity)
        {
            entity.Id = Guid.NewGuid().ToString();
            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = DateTime.UtcNow;
            entity.IsDeleted = false;
            await DbContext.Categories.AddAsync(entity);
            await DbContext.SaveChangesAsync();
            return entity;

        }

        public async Task<Category> DeleteAsync(int id)
        {
            var deleted_category = DbContext.Categories.FirstOrDefault(x => x.Id == id.ToString() && !x.IsDeleted);
            if (deleted_category == null)
            {
                throw new Exception("category not found or already deleted.");
            }
            deleted_category.IsDeleted = true;
            deleted_category.UpdatedAt = DateTime.UtcNow;
            DbContext.Categories.Update(deleted_category);
            await DbContext.SaveChangesAsync();
            return deleted_category;

        }

        public async Task<IEnumerable<Category>> GetAllAsync(int page = 1)
        {
            var categories = await DbContext.Set<Category>().Where(c => !c.IsDeleted).ToListAsync();
            return categories;
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            var category = await DbContext.Set<Category>().Where(c => c.Id == id.ToString() && !c.IsDeleted).FirstOrDefaultAsync();
            return category;

        }
        public async Task<Category> UpdateAsync(Category entity)
        {
            var existingCategory = await DbContext.Categories.FirstOrDefaultAsync(c => c.Id == entity.Id && !c.IsDeleted);
            if (existingCategory == null)
            {
                throw new Exception("Category not found or already deleted.");
            }
            existingCategory.Name = entity.Name;
            existingCategory.UpdatedAt = DateTime.UtcNow;
            DbContext.Categories.Update(existingCategory);

            await DbContext.SaveChangesAsync();

            return existingCategory;

        }
    }
}
