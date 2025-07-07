using Dentizone.Domain.Entity;
using Dentizone.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Dentizone.Domain.Interfaces;

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
            await DbContext.SaveChangesAsync();
            return toBeDeleted;
        }

        public async Task<Review> Update(Review entity)
        {
            DbContext.Reviews.Update(entity);
            await DbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<PagedResult<Review>> FindAllBy(int page, Expression<Func<Review, bool>>? filter)
        {
            var q = DbContext.Reviews
                .Where(r => !r.IsDeleted)
                .AsQueryable();
            var paged = await BuildPagedQuery(page, filter, q);


            var pagedQuery = paged.Query
                .Include(r => r.User)
                .Include(r => r.Order)
                .OrderByDescending(r => r.CreatedAt);

            return new PagedResult<Review>()
            {
                Page = page,
                TotalCount = paged.TotalCount,
                Items = await pagedQuery.ToListAsync(),
                PageSize = DefaultPageSize
            };
        }
    }
}