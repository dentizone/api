using Dentizone.Domain.Entity;
using Dentizone.Domain.Enums;
using System.Linq.Expressions;

namespace Dentizone.Domain.Interfaces.Repositories
{
    public interface IPostRepository : IBaseRepo<Post>
    {
        Task<Post> UpdateAsync(Post entity);
        Task<Post?> DeleteAsync(string id);
        IQueryable<Post> GetTotalPosts();
        Task<IEnumerable<Post>> GetAllAsync(int page);
        Task<Post?> GetBySlugAsync(string slug);

        Task<IEnumerable<Post>> GetAllAsync(int page,
            Expression<Func<Post, bool>>? filter,
            Expression<Func<Post, object>>? orderBy,
            Expression<Func<Post, object>>[]? includes = null
        );

        IQueryable<Post> GetAllAsync(Expression<Func<Post, bool>>? filter,
            Expression<Func<Post, object>>? orderBy = null,
            Expression<Func<Post, object>>[]? includes = null);

        Task<PagedResult<Post>> SearchAsync(string? keyword, string? city, string? category,
            string? subcategory, PostItemCondition? condition,
            decimal? minPrice, decimal? maxPrice, string? sortBy,
            bool sortDirection, int page);

        Task UpdatePostStatus(string postId, PostStatus status);
        IQueryable<Post> GetActivePosts();
        IQueryable<Post> GetPendingPosts();
        Task<decimal> AveragePostsPriceAsync();
        Task<Dictionary<string, int>> GetPostCountPerCategoryAsync();
        Task<IEnumerable<Post>> ValidatePostsByState(List<string> postIds, PostStatus state);
    }
}