using Dentizone.Domain.Entity;

namespace Dentizone.Domain.Interfaces.Repositories
{
    public interface IWithdrawalRequestRepository : IBaseRepo<WithdrawalRequest>
    {
        Task<WithdrawalRequest> UpdateAsync(WithdrawalRequest entity);
        Task<WithdrawalRequest?> DeleteAsync(string id);
        Task<WithdrawalRequest?> GetByIdAsync(string id);
        Task<WithdrawalRequest> CreateAsync(WithdrawalRequest entity);

    }
}