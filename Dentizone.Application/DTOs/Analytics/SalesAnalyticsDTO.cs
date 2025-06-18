using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dentizone.Application.DTOs.Analytics
{
    public class SalesAnalyticsDto
    {
        public decimal TotalSalesRevenue { get; set; }
        public int TotalsOrder { get; set; }
        public decimal AveragePostPrice { get; set; }
    }
}