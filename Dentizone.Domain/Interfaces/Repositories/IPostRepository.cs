using Dentizone.Domain.Entity;
using Dentizone.Domain.Enums;
using System.Linq.Expressions;

namespace Dentizone.Domain.Interfaces.Repositories
{
    public interface IPostRepository : IBaseRepo<Post>
    {
        Task<Post> UpdateAsync(Post entity);
        Task<Post?> DeleteAsync(string id);

        Task<IEnumerable<Post>> GetAllAsync(int page);

        Task<IEnumerable<Post>> GetAllAsync(int page,
                                            Expression<Func<Post, bool>>? filter,
                                            Expression<Func<Post, object>>? orderBy,
                                            Expression<Func<Post, object>>[]? includes = null
        );

        IQueryable<Post> GetAllAsync(Expression<Func<Post, bool>>? filter,
                                     Expression<Func<Post, object>>? orderBy = null,
                                     Expression<Func<Post, object>>[]? includes = null);

        Task<IQueryable<Post>> SearchAsync(string? keyword, string? city, string? category, string? subcategory,
                                           PostItemCondition? condition, decimal? minPrice, decimal? maxPrice,
                                           string? sortBy, bool SortDirection, int page);

        Task UpdatePostStatus(string postId, PostStatus status);
        IQueryable<Post> GetActivePosts();
        IQueryable<Post> GetPendingPosts();
        Task<decimal> AveragePostsPriceAsync();
        Task<Dictionary<string, int>> GetPostCountPerCategoryAsync();
    }
}