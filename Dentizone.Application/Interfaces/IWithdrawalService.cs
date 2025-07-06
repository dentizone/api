using Dentizone.Application.DTOs;
using Dentizone.Application.DTOs.Withdrawal;
using Dentizone.Domain.Enums;

namespace Dentizone.Application.Interfaces
{
    public interface IWithdrawalService
    {
        Task<WithdrawalRequestView> CreateWithdrawalRequestAsync(string userId,
            WithdrawalRequestDto withdrawalRequestDto);

        Task<List<WithdrawalRequestView>> GetWithdrawalHistoryAsync(string userId);
        Task<WithdrawalRequestView> ApproveWithdrawalAsync(string id, string adminNote);
        Task<WithdrawalRequestView> RejectWithdrawalAsync(string id, string adminNote);

        Task<PagedResultDto<FullWithdrawalRequestView>> GetAllWithdrawalsAsync(
            WithdrawalRequestFilterDto dto);

        Task<Dictionary<WithdrawalRequestStatus, int>> GetWithdrawalStatsAsync();
    }
}