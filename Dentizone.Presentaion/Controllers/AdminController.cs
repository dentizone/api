using Dentizone.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dentizone.Presentaion.Controllers
{
    [Route("api/[controller]")]
    [Authorize("IsAdmin")]
    [ApiController]
    public class AdminController(IWithdrawalService withdrawalService) : ControllerBase
    {
        [HttpPost("withdrawal/{id}/approve")]
        public async Task<IActionResult> ApproveWithdrawal(string id)
        {
            var approvedRequest = await withdrawalService.ApproveWithdrawalAsync(id, "adminNote");

            return Ok(approvedRequest);
        }

        [HttpPost("withdrawal/{id}/reject")]
        public async Task<IActionResult> RejectWithdrawal(string id)
        {
            var rejectedRequest = await withdrawalService.RejectWithdrawalAsync(id, "adminNote");
            return Ok(rejectedRequest);
        }
    }
}