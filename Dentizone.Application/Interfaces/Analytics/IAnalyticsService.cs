using Dentizone.Application.DTOs.Analytics;

namespace Dentizone.Application.Interfaces.Analytics
{
    public interface IAnalyticsService
    {
        public Task<UserAnalyticsDto> GetUserAnalyticsAsync();
        public Task<PostAnalyticsDto> GetPostAnalyticsAsync();
        public Task<SalesAnalyticsDto> GetSalesAnalyticsAsync();
    }
}