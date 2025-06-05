using System.Linq.Expressions;
using Dentizone.Application.Abstracts;
using Dentizone.Application.Interfaces;
using Dentizone.Domain.Entity;
using Dentizone.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Dentizone.Application.Repositories
{
    internal class ReviewUXRepository : AbstractRepository, IReviewUXRepository
    {
        private IReviewUXRepository _reviewUxRepositoryImplementation;

        public ReviewUXRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<ReviewUx?> GetByIdAsync(string id)
        {
            return await dbContext.ReviewUxes
                .FirstOrDefaultAsync(r => r.Id == id && !r.IsDeleted);
        }


        public async Task<ReviewUx> CreateAsync(ReviewUx entity)
        {
            await dbContext.ReviewUxes.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<ReviewUx?> FindBy(Expression<Func<ReviewUx, bool>> condition,
            Expression<Func<ReviewUx, object>>[]? includes)
        {
            IQueryable<ReviewUx> query = dbContext.ReviewUxes;
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return await query.FirstOrDefaultAsync(condition);
        }

        public async Task<ReviewUx?> DeleteAsync(string id)
        {
            var toBeDeleted = await GetByIdAsync(id);

            if (toBeDeleted == null)
            {
                return null;
            }

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