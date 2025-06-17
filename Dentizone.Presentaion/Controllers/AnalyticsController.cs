using Dentizone.Application.Interfaces.Analytics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dentizone.Presentaion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnalyticsController : ControllerBase
    {
        private readonly IAnalyticsService _analyticsService;
        public AnalyticsController(IAnalyticsService analyticsService)
        {
            _analyticsService = analyticsService;
        }
        [HttpGet("user")]
        public async Task<IActionResult> GetUserAnalytics()
        {
            var userAnalytics = await _analyticsService.GetUserAnalyticsAsync();
            return Ok(userAnalytics);
        }
        [HttpGet("post")]
        public async Task<IActionResult> GetPostAnalytics()
        {
            var postAnalytics = await _analyticsService.GetPostAnalyticsAsync();
            return Ok(postAnalytics);
        }
        [HttpGet("sales")]
        public async Task<IActionResult> GetSalesAnalytics()
        {
            var salesAnalytics = await _analyticsService.GetSalesAnalyticsAsync();
            return Ok(salesAnalytics);
        }
    }
}
