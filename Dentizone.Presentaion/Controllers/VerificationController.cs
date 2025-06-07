using Dentizone.Application.Services.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dentizone.Presentaion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VerificationController(VerificationService verificationService) : ControllerBase
    {
        private readonly VerificationService _verificationService = verificationService;

        [HttpPost("create")]
        [Authorize]
        public async Task<IActionResult> StartVerification()
        {
            //var session = await _verificationService.StartSessionAsync();

            //return Ok(session);

            return Ok();
        }
    }
}