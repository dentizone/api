using Dentizone.Domain.Entity;
using Dentizone.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Dentizone.Infrastructure.Repositories
{
    internal class ReviewRepository(AppDbContext dbContext) : AbstractRepository(dbContext), IReviewRepository
    {
        public async Task<Review?> GetByIdAsync(string id)
        {
            return await dbContext.Reviews
                                  .FirstOrDefaultAsync(r => r.Id == id && !r.IsDeleted);
        }


        public async Task<Review> CreateAsync(Review entity)
        {
            await dbContext.Reviews.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<Review?> FindBy(Expression<Func<Review, bool>> condition,
                                          Expression<Func<Review, object>>[]? includes)
        {
            IQueryable<Review> query = dbContext.Reviews;
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return await query.FirstOrDefaultAsync(condition);
        }

        public async Task<Review?> DeleteAsync(string id)
        {
            var toBeDeleted = await GetByIdAsync(id);
            if (toBeDeleted == null)
            {
                return null;
            }

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