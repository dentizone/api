using System.Linq.Expressions;
using Dentizone.Domain.Entity;

namespace Dentizone.Domain.Interfaces.Repositories
{
    public interface IWithdrawalRequestRepository : IBaseRepo<WithdrawalRequest>
    {
        Task<WithdrawalRequest> UpdateAsync(WithdrawalRequest entity);

        Task<IEnumerable<WithdrawalRequest>> GetAllAsync(
            Expression<Func<WithdrawalRequest, bool>>? condition);
    }
}