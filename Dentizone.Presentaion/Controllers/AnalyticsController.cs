using Dentizone.Application.Interfaces.Analytics;
using Microsoft.AspNetCore.Mvc;

namespace Dentizone.Presentaion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnalyticsController(IAnalyticsService analyticsService) : ControllerBase
    {
        [HttpGet("user")]
        public async Task<IActionResult> GetUserAnalytics([FromQuery] bool useCache = true)
        {
            var userAnalytics = await analyticsService.GetUserAnalyticsAsync(useCache);
            return Ok(userAnalytics);
        }

        [HttpGet("post")]
        public async Task<IActionResult> GetPostAnalytics([FromQuery] bool useCache = true)
        {
            var postAnalytics = await analyticsService.GetPostAnalyticsAsync(useCache);
            return Ok(postAnalytics);
        }

        [HttpGet("sales")]
        public async Task<IActionResult> GetSalesAnalytics([FromQuery] bool useCache = true)
        {
            var salesAnalytics = await analyticsService.GetSalesAnalyticsAsync(useCache);
            return Ok(salesAnalytics);
        }
    }
}