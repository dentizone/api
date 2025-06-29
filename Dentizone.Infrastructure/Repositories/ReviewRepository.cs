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
            return await DbContext.Reviews
                .FirstOrDefaultAsync(r => r.Id == id && !r.IsDeleted);
        }


        public async Task<Review> CreateAsync(Review entity)
        {
            await DbContext.Reviews.AddAsync(entity);
            await DbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<Review?> FindBy(Expression<Func<Review, bool>> condition,
            Expression<Func<Review, object>>[]? includes)
        {
            IQueryable<Review> query = DbContext.Reviews;
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

            DbContext.Reviews.Remove(toBeDeleted);
            return toBeDeleted;
        }

        public async Task<Review> Update(Review entity)
        {
            DbContext.Reviews.Update(entity);
            await DbContext.SaveChangesAsync();
            return entity;
        }

        public IQueryable<Review> FindAllBy(Expression<Func<Review, bool>> condition)
        {
            return DbContext.Reviews
                .Where(condition).AsQueryable();
        }
    }
}