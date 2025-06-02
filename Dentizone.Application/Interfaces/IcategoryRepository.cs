using Dentizone.Domain.Entity;

namespace Dentizone.Application.Interfaces
{
    public interface ICategoryRepository : IBaseRepo<Category>
    {
        Task<Category?> Update(Category entity);
        Task<Category?> Delete(string id);
        Task<IEnumerable<Category>> GetAll();
    }
}