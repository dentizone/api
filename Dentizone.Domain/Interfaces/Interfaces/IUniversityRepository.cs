using Dentizone.Domain.Entity;

namespace Dentizone.Application.Interfaces;

interface IUniversityRepository : IBaseRepo<University>
{
    Task<University> Update(University entity);
    Task<University?> DeleteAsync(string id);

    Task<ICollection<University>> GetAll();
}