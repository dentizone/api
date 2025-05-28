using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dentizone.Domain.Enums
{
    public enum PaymentStatus
    {
        Success = 0,
        Failed = 1,
        Pending = 2,
        Refunded = 3,
        Cancelled = 4,
    }
}
