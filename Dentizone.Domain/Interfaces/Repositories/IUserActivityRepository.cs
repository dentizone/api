using Dentizone.Domain.Entity;
using System.Linq.Expressions;

namespace Dentizone.Domain.Interfaces.Repositories;

public interface IUserActivityRepository : IBaseRepo<UserActivity>
{
    Task<ICollection<UserActivity>> GetAllBy(int page,
                                             Expression<Func<UserActivity, bool>>? filter);
}