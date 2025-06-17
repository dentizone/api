using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dentizone.Application.DTOs.Analytics;

namespace Dentizone.Application.Interfaces.Analytics
{
   public interface IAnalyticsService
    {
        public Task<UserAnalyticsDTO> GetUserAnalyticsAsync();
        public Task<PostAnalyticsDTO> GetPostAnalyticsAsync();
        public Task<SalesAnalyticsDTO> GetSalesAnalyticsAsync();
        
    }
}
