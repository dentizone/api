using Dentizone.Application.Services.Payment;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Dentizone.Presentaion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController(IWalletService walletService) : ControllerBase
    {
        [HttpGet("balance")]
        public async Task<IActionResult> GetWalletBalance()
        {
            var userId = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;


            var wallet = await walletService.GetWalletBalanceAsync(userId);
            return Ok(wallet);
        }
    }
}