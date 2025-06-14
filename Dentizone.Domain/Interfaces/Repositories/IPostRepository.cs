using Dentizone.Domain.Entity;
using Dentizone.Application.DTOs;
using System.Linq.Expressions;
using Dentizone.Domain.Enums;

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
       Task<IQueryable<Post>> SearchAsync(string? keyword, string? city, string? category, string? subcategory, PostItemCondition? condition, decimal? minPrice, decimal? maxPrice, string? sortBy, bool SortDirection, int page);
    }
}