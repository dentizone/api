using System.Linq.Expressions;
using Dentizone.Domain.Entity;

namespace Dentizone.Domain.Interfaces.Repositories;

public interface IUserRepository : IBaseRepo<AppUser>
{
    Task<AppUser> Update(AppUser entity);
    Task<IEnumerable<AppUser>> GetAllAsync(int page = 1, Expression<Func<AppUser, bool>>? filter = null);

    Task<AppUser?> DeleteAsync(string id);
    Task<int> GetCountOfUsersAsync();
    Task<int> GetCount7DaysAsync();
    Task<int> GetCount30DaysAsync();
}