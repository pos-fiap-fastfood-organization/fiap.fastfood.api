using Adapters.Controllers.Interfaces;
using Adapters.Presenters.Orders;
using Core.UseCases.Interfaces;

namespace Adapters.Controllers
{
    public class PaymentController : IPaymentController
    {
        private readonly IPaymentUseCase _paymentUseCase;

        public PaymentController(IPaymentUseCase paymentUseCase)
        {
            _paymentUseCase = paymentUseCase;
        }

        public async Task ProcessPaymentWebhookAsync(OrderPaymentWebhookRequest paymentWebhookRequest, CancellationToken cancellationToken)
        {
            if (paymentWebhookRequest == null)
            {
                throw new ArgumentException("Payment webhook cannot be null.", nameof(paymentWebhookRequest));
            }

            if (string.IsNullOrWhiteSpace(paymentWebhookRequest.OrderId))
            {
                throw new ArgumentException("Order ID cannot be null or empty.", nameof(paymentWebhookRequest.OrderId));
            }

            await _paymentUseCase.ProcessPaymentAsync(paymentWebhookRequest.OrderId, paymentWebhookRequest.PaymentStatus, cancellationToken);
        }


    }
}
