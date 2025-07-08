using Dentizone.Domain.Entity;
using Dentizone.Domain.Enums;
using Dentizone.Domain.Interfaces;
using Dentizone.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Dentizone.Infrastructure.Repositories
{
    internal class PostRepository(AppDbContext dbContext) : AbstractRepository(dbContext), IPostRepository
    {
        public async Task<Post> CreateAsync(Post entity)
        {
            await DbContext.Posts.AddAsync(entity);
            await DbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<Post?> FindBy(Expression<Func<Post, bool>> condition,
            Expression<Func<Post, object>>[]? includes)
        {
            IQueryable<Post> query = DbContext.Posts;
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

            if (post is not null)
            {
                post.IsDeleted = true;
            }

            await DbContext.SaveChangesAsync();
            return post;
        }


        public async Task<IEnumerable<Post>> GetAllAsync(int page)
        {
            int skippedPages = CalculatePagination(page);
            return await DbContext.Posts
                .Skip(skippedPages)
                .Take(DefaultPageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetAllAsync(int page, Expression<Func<Post, bool>>? filter,
            Expression<Func<Post, object>>? orderBy,
            Expression<Func<Post, object>>[]? includes = null)
        {
            IQueryable<Post> query = DbContext.Posts;
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

        public IQueryable<Post> GetAllAsync(Expression<Func<Post, bool>>? filter,
            Expression<Func<Post, object>>? orderBy = null,
            Expression<Func<Post, object>>[]? includes = null)
        {
            IQueryable<Post> query = DbContext.Posts;
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

            return query;
        }

        public async Task<Post?> GetByIdAsync(string id)
        {
            return await DbContext.Posts
                .Include(p => p.Seller)
                .Include(p => p.Category)
                .Include(p => p.SubCategory)
                .Include(p => p.PostAssets)
                .ThenInclude(p => p.Asset)
                .ThenInclude(p => p.User.University)
                .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted && p.Status == PostStatus.Active);
        }

        public async Task<Post?> GetBySlugAsync(string slug)
        {
            return await DbContext.Posts
                .Include(p => p.Seller)
                .Include(p => p.Category)
                .Include(p => p.SubCategory)
                .Include(p => p.PostAssets)
                .ThenInclude(p => p.Asset)
                .ThenInclude(p => p.User.University)
                .FirstOrDefaultAsync(p => p.Slug == slug && !p.IsDeleted && p.Status == PostStatus.Active);
        }

        public async Task<Post> UpdateAsync(Post entity)
        {
            DbContext.Posts.Update(entity);
            await DbContext.SaveChangesAsync();
            return entity;
        }

        public async Task UpdatePostStatus(string postId, PostStatus status)
        {
            var post = await GetByIdAsync(postId);
            if (post == null)
            {
                throw new KeyNotFoundException($"Post with ID {postId} not found.");
            }

            post.Status = status;
            DbContext.Posts.Update(post);
            await DbContext.SaveChangesAsync();
        }

        public async Task<PagedResult<Post>> SearchAsync(string? keyword, string? city, string? category,
            string? subcategory, PostItemCondition? condition,
            decimal? minPrice, decimal? maxPrice, string? sortBy,
            bool sortDirection, int page, PostStatus status)
        {
            var posts = DbContext.Posts
                .Where(p => !p.IsDeleted && p.Status == status);


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
                posts = sortDirection ? posts.OrderBy(p => p.Price) : posts.OrderByDescending(p => p.Price);
            }
            else
            {
                posts = sortDirection ? posts.OrderBy(p => p.CreatedAt) : posts.OrderByDescending(p => p.CreatedAt);
            }

            var totalCount = await posts.CountAsync();

            posts = posts.Skip(CalculatePagination(page > 0 ? page : 1)).Take(DefaultPageSize);

            posts = posts.Include(p => p.PostAssets).ThenInclude(pa => pa.Asset)
                .Include(p => p.Seller)
                .ThenInclude(p => p.University)
                .Include(p => p.Category)
                .Include(p => p.SubCategory);
            var result = await posts.ToListAsync();
            return new PagedResult<Post>
            {
                Items = result,
                TotalCount = totalCount,
                PageSize = DefaultPageSize,
                Page = page
            };
        }

        public IQueryable<Post> GetActivePosts()
        {
            var result = DbContext.Posts.AsNoTracking().Where(p => !p.IsDeleted && p.Status == PostStatus.Active);

            return result;
        }

        public IQueryable<Post> GetTotalPosts()
        {
            var result = DbContext.Posts.AsNoTracking().Where(p => !p.IsDeleted);

            return result;
        }

        public IQueryable<Post> GetPendingPosts()
        {
            return DbContext.Posts.AsNoTracking().Where(p => !p.IsDeleted && p.Status == PostStatus.Pending);
        }

        public async Task<decimal> AveragePostsPriceAsync()
        {
            var posts = GetActivePosts();

            if (!await posts.AnyAsync())
            {
                return 0;
            }

            return await posts.AverageAsync(p => p.Price);
        }

        public async Task<Dictionary<string, int>> GetPostCountPerCategoryAsync()
        {
            var result = await DbContext.Posts
                .AsNoTracking()
                .Include(p => p.Category)
                .Where(p => !p.IsDeleted && !p.Category.IsDeleted)
                .GroupBy(p => p.Category.Name)
                .AsSplitQuery()
                .ToDictionaryAsync(g => g.Key, g => g.Count());

            return result;
        }

        public async Task<IEnumerable<Post>> ValidatePostsByState(List<string> postIds, PostStatus state)
        {
            var posts = await DbContext.Posts
                .Where(p => postIds.Contains(p.Id) && p.Status == state)
                .Include(p => p.Seller)
                .ThenInclude(s => s.Wallet)
                .ToListAsync();

            return posts;
        }
    }
}