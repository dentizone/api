using Dentizone.Domain.Entity;
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
    }
}