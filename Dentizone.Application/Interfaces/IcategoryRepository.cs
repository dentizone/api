using Dentizone.Domain.Entity;

namespace Dentizone.Application.Interfaces
{
    internal interface ICategoryRepository : IBaseRepo<Category>
    {
        Task<Category?> UpdateAsync(Category entity);
    }
}