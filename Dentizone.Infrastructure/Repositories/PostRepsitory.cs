using Dentizone.Domain.Entity;
using Dentizone.Domain.Enums;
using Dentizone.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Dentizone.Infrastructure.Repositories
{
    internal class PostRepository : AbstractRepository, IPostRepository
    {
        public PostRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Post> CreateAsync(Post entity)
        {
            await dbContext.Posts.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<Post?> FindBy(Expression<Func<Post, bool>> condition,
                                        Expression<Func<Post, object>>[]? includes)
        {
            IQueryable<Post> query = dbContext.Posts;
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return await query.FirstOrDefaultAsync(condition);
        }

        public async Task<Post?> DeleteAsync(string id)
        {
            var post = await GetByIdAsync(id);

            if (post == null)
            {
                return null;
            }


            dbContext.Posts.Remove(post);
            await dbContext.SaveChangesAsync();
            return post;
        }

        public async Task<IEnumerable<Post>> GetAllAsync(int page = 1)
        {
            int skippedPages = CalculatePagination(page);
            return await dbContext.Posts
                                  .Skip(skippedPages)
                                  .Take(DefaultPageSize)
                                  .ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetAllAsync(int page, Expression<Func<Post, bool>>? filter,
                                                         Expression<Func<Post, object>>? orderBy,
                                                         Expression<Func<Post, object>>[]? includes = null)
        {
            IQueryable<Post> query = dbContext.Posts;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            if (orderBy != null)
            {
                query = query.OrderBy(orderBy);
            }

            var skippedPages = CalculatePagination(page);
            return await query.Skip(skippedPages).Take(DefaultPageSize).ToListAsync();
        }

        public async Task<Post?> GetByIdAsync(string id)
        {
            return await dbContext.Posts.FindAsync(id);
        }

        public async Task<Post> UpdateAsync(Post entity)
        {
            dbContext.Posts.Update(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<IQueryable<Post>> SearchAsync(string? keyword, string? city, string? category, string? subcategory, PostItemCondition? condition, decimal? minPrice, decimal? maxPrice, string? sortBy, bool SortDirection, int page)
        {
            var posts = dbContext.Posts
                .Where(p => !p.IsDeleted && p.Status == PostStatus.Active)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                var kw = keyword.Trim().ToLower();
                posts = posts.Where(p =>
                    p.Title.ToLower().Contains(kw) ||
                    p.Description.ToLower().Contains(kw));
            }

            if (!string.IsNullOrWhiteSpace(city))
                posts = posts.Where(p => p.City == city);
            if (!string.IsNullOrWhiteSpace(category))
                posts = posts.Where(p => p.Category.Name == category);
            if (!string.IsNullOrWhiteSpace(subcategory))
                posts = posts.Where(p => p.SubCategory.Name == subcategory);
            if (condition.HasValue)
                posts = posts.Where(p => p.Condition == condition.Value);
            if (minPrice.HasValue)
                posts = posts.Where(p => p.Price >= minPrice.Value);
            if (maxPrice.HasValue)
                posts = posts.Where(p => p.Price <= maxPrice.Value);

            if (sortBy?.ToLower() == "price")
            {
                posts = SortDirection ? posts.OrderBy(p => p.Price) : posts.OrderByDescending(p => p.Price);
            }
            else
            {
                posts = SortDirection ? posts.OrderBy(p => p.CreatedAt) : posts.OrderByDescending(p => p.CreatedAt);
            }

            var skippedPages = CalculatePagination(page);
            return posts.Skip(skippedPages).Take(DefaultPageSize);
        }
    }
}