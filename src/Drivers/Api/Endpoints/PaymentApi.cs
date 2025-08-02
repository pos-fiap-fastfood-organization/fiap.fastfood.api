using Adapters.Controllers.Interfaces;
using Adapters.Presenters.Orders;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints
{
    [ApiController]
    [Route("payment")]
    public class PaymentApi : ControllerBase
    {
        private readonly IPaymentController _paymentController;

        public PaymentApi(IPaymentController paymentController)
        {
            _paymentController = paymentController;
        }

        [HttpPost("webhook")]
        public async Task<IActionResult> PaymentWebhookAsync(
        [FromBody] OrderPaymentWebhookRequest paymentWebhookRequest,
        CancellationToken cancellationToken)
        {
            await _paymentController.ProcessPaymentWebhookAsync(paymentWebhookRequest, cancellationToken);
            return NoContent();
        }
    }
}
