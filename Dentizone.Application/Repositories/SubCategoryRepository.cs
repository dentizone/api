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

        public async Task<IEnumerable<SubCategory>> GetAllAsync(int page = 1)
        {
            return await dbContext.SubCategories.Where(c => !c.IsDeleted)
                                  .Skip(CalculatePagination(page))
                                  .Take(DefaultPageSize)
                                  .ToListAsync();
        }

        public async Task<SubCategory> CreateAsync(SubCategory entity)
        {
            await dbContext.SubCategories.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<SubCategory?> DeleteAsync(string id)
        {
            var toBeDeleted = await GetByIdAsync(id);

            dbContext.SubCategories.Remove(toBeDeleted);

            return toBeDeleted;
        }

        public async Task<SubCategory?> Update(SubCategory entity)
        {
            dbContext.SubCategories.Update(entity);
            await dbContext.SaveChangesAsync();

            return entity;
        }
    }
}