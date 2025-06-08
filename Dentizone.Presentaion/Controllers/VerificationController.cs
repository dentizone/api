using Dentizone.Application.Interfaces.User;
using Dentizone.Application.Services.Authentication;
using Dentizone.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace Dentizone.Presentaion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VerificationController(VerificationService verificationService, IUserService userService)
        : ControllerBase
    {
        [HttpPost("create")]
        [Authorize]
        public async Task<IActionResult> StartVerification()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("User ID is required.");
            }

            // Check if the user already verifed
            var user = await userService.GetByIdAsync(userId);

            if (user.KycStatus != KycStatus.PENDING)
            {
                return BadRequest("User is already verified.");
            }


            var session = await verificationService.StartSessionAsync(userId);

            return Ok(session);

            // return Ok();
        }

        [HttpGet("status")]
        [Authorize]
        public async Task<IActionResult> GetVerificationStatus([FromQuery] string sessionId)
        {
            // Assuming you have a method to get the verification status by sessionId
            var status = await verificationService.GetVerificationStatusAsync(sessionId);
            if (status == null)
            {
                return NotFound("Verification session not found.");
            }

            return Ok(status);
        }

        [HttpPost("alert")]
        public async Task<IActionResult> AlertWebhook()
        {
            try
            {
                // 1) Read raw body
                Request.EnableBuffering(); // Allows re-reading the stream
                using var reader = new StreamReader(Request.Body, Encoding.UTF8, leaveOpen: true);
                var rawBody = await reader.ReadToEndAsync();
                Request.Body.Position = 0;

                // 2) Extract headers
                if (!Request.Headers.TryGetValue("X-Signature", out StringValues signatureHeader) ||
                    !Request.Headers.TryGetValue("X-Timestamp", out StringValues timestampHeader))
                {
                    return Unauthorized("Missing headers");
                }

                var signature = signatureHeader.ToString();
                var timestamp = timestampHeader.ToString();

                // 3) Validate timestamp
                // if (!long.TryParse(timestamp, out long incomingTimestamp))
                //     return Unauthorized("Invalid timestamp");
                //
                // var currentTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                // if (Math.Abs(currentTimestamp - incomingTimestamp) > 300)
                //     return Unauthorized("Stale request");


                // 6) Deserialize body and process event
                var jsonDoc = JsonDocument.Parse(rawBody);
                var root = jsonDoc.RootElement;

                var sessionId = root.GetProperty("session_id").GetString();
                var status = root.GetProperty("status").GetString();
                var userId = root.GetProperty("vendor_data").ToString();

                await verificationService.UpdateUserVerificationState(userId, status);


                // TODO: Handle your logic here (e.g., upsertVerification(sessionId, status, vendorData, workflowId))

                return Ok(new { message = "Webhook event dispatched" });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in /webhook handler: " + ex.Message);
                return Unauthorized("Invalid request");
            }
        }
    }
}