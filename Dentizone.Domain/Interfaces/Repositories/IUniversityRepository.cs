using Dentizone.Domain.Entity;
using System.Linq.Expressions;

namespace Dentizone.Domain.Interfaces.Repositories;

public interface IUniversityRepository : IBaseRepo<University>
{
    Task<University> Update(University entity);
    Task<University?> DeleteAsync(string id);
    Task<IReadOnlyCollection<University>> GetAll();
    Task<PagedResult<University>> GetAll(int page, Expression<Func<University, bool>>? filter);
}