using Dentizone.Application.Interfaces.Analytics;
using Microsoft.AspNetCore.Mvc;

namespace Dentizone.Presentaion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnalyticsController(IAnalyticsService analyticsService) : ControllerBase
    {
        [HttpGet("user")]
        public async Task<IActionResult> GetUserAnalytics()
        {
            var userAnalytics = await analyticsService.GetUserAnalyticsAsync();
            return Ok(userAnalytics);
        }

        [HttpGet("post")]
        public async Task<IActionResult> GetPostAnalytics()
        {
            var postAnalytics = await analyticsService.GetPostAnalyticsAsync();
            return Ok(postAnalytics);
        }

        [HttpGet("sales")]
        public async Task<IActionResult> GetSalesAnalytics()
        {
            var salesAnalytics = await analyticsService.GetSalesAnalyticsAsync();
            return Ok(salesAnalytics);
        }
    }
}