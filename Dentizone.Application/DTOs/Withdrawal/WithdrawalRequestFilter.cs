using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dentizone.Domain.Enums;

namespace Dentizone.Application.DTOs.Withdrawal
{
    public class WithdrawalRequestFilterDto
    {
        public WithdrawalRequestStatus? RequestStatus { get; set; }
        public string? SearchTerm { get; set; }
        public DateTime? RequestDateTime { get; set; }
        public int Page { get; set; }
    }
}