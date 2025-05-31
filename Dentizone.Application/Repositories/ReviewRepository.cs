using Dentizone.Application.Abstracts;
using Dentizone.Application.Interfaces;
using Dentizone.Domain.Entity;
using Dentizone.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Dentizone.Application.Repositories
{
    internal class ReviewRepository : AbstractRepository, IReviewRepository
    {
        public ReviewRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Review?> GetByIdAsync(string id)
        {
            return await dbContext.Reviews
                                  .FirstOrDefaultAsync(r => r.Id == id && !r.IsDeleted);
        }

        public async Task<IEnumerable<Review>> GetAllAsync(int page = 1)
        {
            return await dbContext.Reviews
                                  .Where(r => !r.IsDeleted)
                                  .Skip(CalculatePagination(page))
                                  .Take(DefaultPageSize)
                                  .ToListAsync();
        }

        public async Task<Review> CreateAsync(Review entity)
        {
            await dbContext.Reviews.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<Review?> DeleteAsync(string id)
        {
            var toBeDeleted = await GetByIdAsync(id);
            dbContext.Reviews.Remove(toBeDeleted);
            return toBeDeleted;
        }

        public async Task<Review> Update(Review entity)
        {
            dbContext.Reviews.Update(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }
    }
}