using Dentizone.Domain.Entity;

namespace Dentizone.Application.Interfaces
{
    internal interface IWithdrawalRequestRepository : IBaseRepo<WithdrawalRequest>
    {
        Task<WithdrawalRequest> UpdateAsync(WithdrawalRequest entity);
    }
}