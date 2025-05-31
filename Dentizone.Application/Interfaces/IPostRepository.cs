using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dentizone.Application.Repositories;
using Dentizone.Domain.Entity;

namespace Dentizone.Application.Interfaces
{
    internal interface IPostRepository: IBaseRepo<Post>
    {
        Task<Post> UpdateAsync(Post entity);
    }
}
