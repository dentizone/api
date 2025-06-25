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
        Task<WithdrawalRequest> CreateWithdrawalRequestAsync(string userId, WithdrawalRequestDto withdrawalRequestDto);
    }
}
}
