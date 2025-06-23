using Dentizone.Application.DTOs.Analytics;

namespace Dentizone.Application.Interfaces.Analytics
{
    public interface IAnalyticsService
    {
        public Task<UserAnalyticsDto> GetUserAnalyticsAsync(bool useCache = false);
        public Task<PostAnalyticsDto> GetPostAnalyticsAsync(bool useCache = false);
        public Task<SalesAnalyticsDto> GetSalesAnalyticsAsync(bool useCache = false);
    }
}