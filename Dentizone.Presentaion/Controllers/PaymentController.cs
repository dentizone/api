using Dentizone.Application.Services.Payment;
using Microsoft.AspNetCore.Mvc;

namespace Dentizone.Presentaion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController(IPaymentService paymentService) : ControllerBase
    {
        [HttpGet]
        [Route("confirm")]
        public async Task<IActionResult> ConfirmPayment(string paymentId, string orderId)
        {
            return NoContent();
        }
    }
}