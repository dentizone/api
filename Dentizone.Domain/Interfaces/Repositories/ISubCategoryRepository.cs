using Dentizone.Domain.Entity;

namespace Dentizone.Domain.Interfaces.Repositories;

public interface ISubCategoryRepository : IBaseRepo<SubCategory>
{
    Task<SubCategory?> Update(SubCategory entity);
    Task<SubCategory?> DeleteAsync(string id);
    Task<ICollection<SubCategory>> GetAll();
}