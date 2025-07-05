using Dentizone.Application.Interfaces;
using Dentizone.Domain.Interfaces;
using Dentizone.Infrastructure.ApiClient;
using Microsoft.AspNetCore.Mvc;

namespace Dentizone.Presentaion.Controllers
{
    public class TestPayload
    {
        public string text { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class TestController(IAiLayer ailayer, IBackgroundJobService _backgroundJobService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Get([FromBody] TestPayload payload)
        {
            //          var result = await ailayer.ScanAll(payload.text);
            //        return Ok(result.Content);
            // For testing background job
            _backgroundJobService.Enqueue<IMonitorJob>((job) => job.ReviewReviewAsync("01802deb-7008-4269-b3b4-a9c2a23fb4ec", "Fuck this product"));

            return Ok();
        }
    }
}