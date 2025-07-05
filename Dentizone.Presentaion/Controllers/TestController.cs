using Dentizone.Application.Interfaces;
using Dentizone.Domain.Interfaces;
using Dentizone.Infrastructure.ApiClient;
using Dentizone.Infrastructure.Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace Dentizone.Presentaion.Controllers
{
    public class TestPayload
    {
        public string text { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class TestController(IAILayer ailayer, IBackgroundJobService _backgroundJobService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Get([FromBody] TestPayload payload)
        {
            //          var result = await ailayer.ScanAll(payload.text);
            //        return Ok(result.Content);
            // For testing background job
            // await ailayer.ScanAll("A7a");
            _backgroundJobService.Enqueue<IMoitorJob>(job => job.ReviewToxicAndPII("This is Hello world",
                                                           "Resource 123", "Commecnt"));

            return Ok();
        }
    }
}