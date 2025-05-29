using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dentizone.Application.Interfaces
{
    internal interface IBaseRepo<TEntity> where TEntity : class
    {
        Task<TEntity> GetByIdAsync(int id);

        Task<IEnumerable<TEntity>> GetAllAsync(int page = 1);
        Task<TEntity> CreateAsync<TC>(TC entity);
        Task<TEntity> UpdateAsync<TU>(int id, TU UpdatePayload);
        Task<TEntity> DeleteAsync(int id);
    }
}