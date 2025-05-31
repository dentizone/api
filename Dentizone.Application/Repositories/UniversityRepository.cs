using Dentizone.Application.Abstracts;
using Dentizone.Application.Interfaces;
using Dentizone.Domain.Entity;
using Dentizone.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Dentizone.Application.Repositories
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

        public async Task<IEnumerable<University>> GetAllAsync(int page = 1)
        {
            return await dbContext.Universities
                                  .Where(u => !u.IsDeleted)
                                  .Skip(CalculatePagination(page))
                                  .Take(DefaultPageSize)
                                  .ToListAsync();
        }

        public async Task<University> CreateAsync(University entity)
        {
            await dbContext.Universities.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<University?> DeleteAsync(string id)
        {
            var toBeDeleted = await GetByIdAsync(id);

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
    }
}