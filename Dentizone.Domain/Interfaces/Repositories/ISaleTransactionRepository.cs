using Dentizone.Domain.Entity;

namespace Dentizone.Domain.Interfaces.Repositories;

public interface ISaleTransactionRepository : IBaseRepo<SalesTransaction>
{
    Task<SalesTransaction> UpdateAsync(SalesTransaction entity);
}