using Dentizone.Domain.Entity;

namespace Dentizone.Application.Interfaces;

interface ISubCategoryRepository : IBaseRepo<SubCategory>
{
    Task<SubCategory?> Update(SubCategory entity);
}