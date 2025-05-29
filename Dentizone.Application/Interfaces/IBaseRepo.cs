namespace Dentizone.Application.Interfaces
{
    internal interface IBaseRepo<TEntity> where TEntity : class
    {
        Task<TEntity> GetByIdAsync(int id);
        Task<IEnumerable<TEntity>> GetAllAsync(int page = 1);
        Task<TEntity> CreateAsync(TEntity entity);
        Task<TEntity> DeleteAsync(int id);
    }
}