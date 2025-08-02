using Adapters.Presenters.Orders;

namespace Adapters.Controllers.Interfaces
{
    public interface IPaymentController
    {
        Task ProcessPaymentWebhookAsync(OrderPaymentWebhookRequest paymentWebhookRequest, CancellationToken cancellationToken);
    }
}
