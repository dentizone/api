using Dentizone.Application.DTOs.Withdrawal;
using Dentizone.Application.Interfaces;
using Dentizone.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dentizone.Presentaion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController(IWithdrawalService withdrawalService) : ControllerBase
    {
        [HttpPut("withdrawal/{id}/approve")]
        public async Task<IActionResult> ApproveWithdrawal(string id, [FromBody] string adminNote)
        {
            var approvedRequest = await withdrawalService.ApproveWithdrawalAsync(id, adminNote);

            return Ok(approvedRequest);
        }

        [HttpPut("withdrawal/{id}/reject")]
        public async Task<IActionResult> RejectWithdrawal(string id, [FromBody] string adminNote)
        {
            var rejectedRequest = await withdrawalService.RejectWithdrawalAsync(id, adminNote);
            return Ok(rejectedRequest);
        }
    }
}
