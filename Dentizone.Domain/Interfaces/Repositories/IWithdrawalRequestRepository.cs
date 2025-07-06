using System.Linq.Expressions;
using Dentizone.Domain.Entity;

namespace Dentizone.Domain.Interfaces.Repositories
{
    public interface IWithdrawalRequestRepository : IBaseRepo<WithdrawalRequest>
    {
        Task<WithdrawalRequest> UpdateAsync(WithdrawalRequest entity);

        Task<PagedResult<WithdrawalRequest>> GetAllAsync(int page,
            Expression<Func<WithdrawalRequest, bool>>? condition);

        Task<IEnumerable<WithdrawalRequest>> GetAllAsync(
            Expression<Func<WithdrawalRequest, bool>>? condition = null);
    }
}