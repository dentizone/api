using Dentizone.Application.Abstracts;
using Dentizone.Application.Interfaces;
using Dentizone.Domain.Entity;
using Dentizone.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Dentizone.Application.Repositories
{
    internal class ReviewUXRepository : AbstractRepository, IReviewUXRepository
    {
        public ReviewUXRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<ReviewUx?> GetByIdAsync(string id)
        {
            return await dbContext.ReviewUxes
                                  .FirstOrDefaultAsync(r => r.Id == id && !r.IsDeleted);
        }

        public async Task<IEnumerable<ReviewUx>> GetAllAsync(int page = 1)
        {
            return await dbContext.ReviewUxes
                                  .Where(r => !r.IsDeleted)
                                  .Skip(CalculatePagination(page))
                                  .Take(DefaultPageSize)
                                  .ToListAsync();
        }

        public async Task<ReviewUx> CreateAsync(ReviewUx entity)
        {
            await dbContext.ReviewUxes.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<ReviewUx?> DeleteAsync(string id)
        {
            var toBeDeleted = await GetByIdAsync(id);
            dbContext.ReviewUxes.Remove(toBeDeleted);
            return toBeDeleted;
        }

        public async Task<ReviewUx> Update(ReviewUx entity)
        {
            dbContext.ReviewUxes.Update(entity);
            await dbContext.SaveChangesAsync();

            return entity;
        }
    }
}