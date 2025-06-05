using Dentizone.Domain.Entity;

namespace Dentizone.Application.Interfaces;

public interface ISubCategoryRepository : IBaseRepo<SubCategory>
{
    Task<SubCategory?> Update(SubCategory entity);
    Task<SubCategory?> DeleteAsync(string id);
    Task<ICollection<SubCategory>> GetAll();
}