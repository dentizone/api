using Dentizone.Application.DTOs.Withdrawal;
using Dentizone.Application.Interfaces;
using Dentizone.Application.Services.Payment;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Dentizone.Presentaion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WalletController(IWalletService walletService, IWithdrawalService withdrawalService) : ControllerBase
    {
        [HttpGet("balance")]
        public async Task<IActionResult> GetWalletBalance()
        {
            var userId = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;


            var wallet = await walletService.GetWalletBalanceAsync(userId);
            return Ok(wallet);
        }

        [HttpPost("withdraw")]
        public async Task<IActionResult> RequestWithdrawal([FromBody] WithdrawalRequestDto withdrawalRequestDto)
        {
            var userId = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

            var request = await withdrawalService.CreateWithdrawalRequestAsync(userId, withdrawalRequestDto);
            return Ok(request);
        }

        [HttpGet("withdrawal-history")]
        public async Task<IActionResult> GetWithdrawalHistory(int page = 1)
        {
            var userId = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var history = await withdrawalService.GetWithdrawalHistoryAsync(userId);
            return Ok(history);
        }

        [Authorize("IsAdmin")]
        [HttpPost("withdrawal/{id}/approve")]
        public async Task<IActionResult> ApproveWithdrawal(string id, [FromBody] string note = "")
        {
            var approvedRequest = await withdrawalService.ApproveWithdrawalAsync(id, note);

            return Ok(approvedRequest);
        }

        [Authorize("IsAdmin")]
        [HttpPost("withdrawal/{id}/reject")]
        public async Task<IActionResult> RejectWithdrawal(string id, [FromBody] string note = "")
        {
            var rejectedRequest = await withdrawalService.RejectWithdrawalAsync(id, note);
            return Ok(rejectedRequest);
        }

        [Authorize("IsAdmin")]
        [HttpGet("all")]
        public async Task<IActionResult> GetAllWithdrawals([FromQuery] WithdrawalRequestFilterDto dto)
        {
            var withdrawals = await withdrawalService.GetAllWithdrawalsAsync(dto);
            return Ok(withdrawals);
        }
    }
}