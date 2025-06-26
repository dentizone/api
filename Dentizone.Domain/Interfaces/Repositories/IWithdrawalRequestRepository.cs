using System.Linq.Expressions;
using Dentizone.Domain.Entity;

namespace Dentizone.Domain.Interfaces.Repositories
{
    public interface IWithdrawalRequestRepository : IBaseRepo<WithdrawalRequest>
    {
        Task<WithdrawalRequest> UpdateAsync(WithdrawalRequest entity);
        Task<WithdrawalRequest?> DeleteAsync(string id);
        Task<WithdrawalRequest?> GetByIdAsync(string id);
        Task<WithdrawalRequest> CreateAsync(WithdrawalRequest entity);
        public Task<WithdrawalRequest?> FindBy(Expression<Func<WithdrawalRequest, bool>> condition,
                                                    Expression<Func<WithdrawalRequest, object>>[]? includes);
        public Task<IEnumerable<WithdrawalRequest>> GetAllAsync(int page, Expression<Func<WithdrawalRequest, bool>>? condition = null);
    }
}