using Dentizone.Domain.Entity;

namespace Dentizone.Application.Interfaces;

interface ISaleTransactionRepository : IBaseRepo<SalesTransaction>
{
    Task<SalesTransaction> UpdateAsync(SalesTransaction entity);
}