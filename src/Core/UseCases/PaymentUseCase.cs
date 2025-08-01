using Core.Entities;
using Core.Entities.Enums;
using Core.Exceptions;
using Core.Gateways.Interfaces;
using Core.UseCases.Exceptions;
using Core.UseCases.Interfaces;

namespace Core.UseCases
{
    public class PaymentUseCase : IPaymentUseCase
    {
        private const string PAYMENT_REFUSED_REASON = "Payment refused.";
        private readonly IPaymentGateway _paymentGateway;
        private readonly IOrderGateway _orderGateway;

        public PaymentUseCase(IPaymentGateway paymentGateway, IOrderGateway ordergateway)
        {
            _paymentGateway = paymentGateway;
            _orderGateway = ordergateway;
        }

        public Task<OrderPayment> CreateOrderPaymentAsync(Order order, Customer? customer, CancellationToken cancellationToken)
        {
            return _paymentGateway.CreatePaymentAsync(
                order,
                customer,
                order.PaymentMethod,
                cancellationToken);
        }

        public async Task ProcessPaymentAsync(string id, PaymentStatus paymentStatus, CancellationToken cancellationToken)
        {
            InvalidPaymentStatusException.ThrowIfInvalidStatus(paymentStatus);

            var order = await _orderGateway.GetByIdAsync(id, cancellationToken);
            OrderNotFoundException.ThrowIfNullOrEmpty(id, order);

            switch (paymentStatus)
            {
                case PaymentStatus.Approved:
                    await SetConfirmationOrderPaymentAsync(order!, cancellationToken);
                    break;
                case PaymentStatus.Refused:
                    await SetRefusalOrderPaymentAsync(order!, cancellationToken);
                    break;
                default:
                    throw new InvalidPaymentProcessingException();
            }
        }

        private async Task SetConfirmationOrderPaymentAsync(Order order, CancellationToken cancellationToken)
        {
            order!.ConfirmPayment();

            await _orderGateway.UpdateStatusAsync(order.Id!, order.Status, cancellationToken);
        }

        private async Task SetRefusalOrderPaymentAsync(Order order, CancellationToken cancellationToken)
        {
            order!.Cancel(PAYMENT_REFUSED_REASON);

            await _orderGateway.UpdateStatusAsync(order.Id!, order.Status, order.Notes, cancellationToken);
        }
    }
}
