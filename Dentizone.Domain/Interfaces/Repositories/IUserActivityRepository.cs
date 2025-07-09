using System.Linq.Expressions;
using Dentizone.Domain.Entity;

namespace Dentizone.Domain.Interfaces.Repositories;

public interface IUserActivityRepository : IBaseRepo<UserActivity>
{
    Task<ICollection<UserActivity>> GetAllBy(int page,
        Expression<Func<UserActivity, bool>>? filter);

    Task<PagedResult<UserActivity>> GetAllAsync(int page,
        Expression<Func<UserActivity, bool>>? filters);
}