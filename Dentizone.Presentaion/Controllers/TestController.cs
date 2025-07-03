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
    public class TestController(IAILayer ailayer) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Get([FromBody] TestPayload payload)
        {
            var result = await ailayer.ScanAll(payload.text);
            return Ok(result.Content);
        }
    }
}