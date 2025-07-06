using Dentizone.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dentizone.Presentaion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("IsAdmin")]
    public class AnalyticsController(IAnalyticsService analyticsService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetDashboardStats([FromQuery] bool useCache = true)
        {
            var userAnalytics = await analyticsService.GetUserAnalyticsAsync(useCache);
            var postAnalytics = await analyticsService.GetPostAnalyticsAsync(useCache);
            var salesAnalytics = await analyticsService.GetSalesAnalyticsAsync(useCache);

            var dashboardStats = new
            {
                UserAnalytics = userAnalytics,
                PostAnalytics = postAnalytics,
                SalesAnalytics = salesAnalytics,
            };
            return Ok(dashboardStats);
        }
    }
}