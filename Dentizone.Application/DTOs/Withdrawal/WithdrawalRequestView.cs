using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dentizone.Application.DTOs.Withdrawal
{
    public class WithdrawalRequestView
    {
        public string Id { get; set; }
        public decimal Amount { get; set; }
        public string WalletId { get; set; }
        public string Status { get; set; } // e.g., Pending, Completed, Failed
        public string UserId { get; set; } // The ID of the user who made the withdrawal request
    }
}
