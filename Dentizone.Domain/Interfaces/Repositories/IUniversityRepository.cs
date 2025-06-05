using Dentizone.Domain.Entity;

namespace Dentizone.Domain.Interfaces.Repositories;

public interface IUniversityRepository : IBaseRepo<University>
{
    Task<University> Update(University entity);
    Task<University?> DeleteAsync(string id);

    Task<ICollection<University>> GetAll();
}