using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Dentizone.Application.Repositories;
using Dentizone.Domain.Entity;

namespace Dentizone.Application.Interfaces
{
    internal interface IPostRepository : IBaseRepo<Post>
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