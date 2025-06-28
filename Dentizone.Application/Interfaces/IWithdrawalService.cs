using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dentizone.Application.DTOs.Withdrawal;
using Dentizone.Domain.Entity;

namespace Dentizone.Application.Interfaces
{
    public interface IWithdrawalService
    {
        Task<WithdrawalRequestView> CreateWithdrawalRequestAsync(string userId, WithdrawalRequestDto withdrawalRequestDto);
        Task<List<WithdrawalRequestView>> GetWithdrawalHistoryAsync(string userId, int page);
        Task<WithdrawalRequestView> ApproveWithdrawalAsync(string id, string adminNote);
        Task<WithdrawalRequestView> RejectWithdrawalAsync(string id, string adminNote);
    }
}


