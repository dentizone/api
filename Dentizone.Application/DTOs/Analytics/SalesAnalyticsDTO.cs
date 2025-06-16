using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dentizone.Application.DTOs.Analytics
{
    public class SalesAnalyticsDTO
    {
        public int TotalSalesRevenue { get; set; }
        public int TotalsOrder { get; set; }
        public int AveragePostPrice { get; set; }

    }
}
