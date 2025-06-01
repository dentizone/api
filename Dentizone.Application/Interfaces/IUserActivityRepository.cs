using System.Linq.Expressions;
using Dentizone.Domain.Entity;

namespace Dentizone.Application.Interfaces;

interface IUserActivityRepository : IBaseRepo<UserActivity>
{
    Task<ICollection<UserActivity>> GetAllBy(int page,
        Expression<Func<UserActivity, bool>>? filter);
}