using System.Linq.Expressions;
using Dentizone.Domain.Entity;

namespace Dentizone.Application.Interfaces
{
    public interface IBaseRepo<TEntity> where TEntity : class
    {
        Task<TEntity?> GetByIdAsync(string id);
        Task<TEntity> CreateAsync(TEntity entity);

        Task<TEntity?> FindBy(Expression<Func<TEntity, bool>> condition, Expression<Func<TEntity, object>>[]? includes);
    }
}