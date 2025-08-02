using Core.Entities;
using Core.Entities.Enums;
using Core.Exceptions;
using Core.Gateways.Interfaces;
using Core.UseCases.Exceptions;
using Core.UseCases.Factories.Interfaces;
using Core.UseCases.Interfaces;

namespace Core.UseCases
{
    public class PaymentUseCase : IPaymentUseCase
    {
        private readonly IPaymentGateway _paymentGateway;
        private readonly IOrderUseCaseFactory _orderUseCaseFactory;

        public PaymentUseCase(IPaymentGateway paymentGateway, IOrderUseCaseFactory orderUseCaseFactory)
        {
            _paymentGateway = paymentGateway;
            _orderUseCaseFactory = orderUseCaseFactory;
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

            var orderUseCase = _orderUseCaseFactory.Create();

            var order = await orderUseCase.GetByIdAsync(id, cancellationToken);
            OrderNotFoundException.ThrowIfNullOrEmpty(id, order);

            switch (paymentStatus)
            {
                case PaymentStatus.Approved:
                    await orderUseCase.SetConfirmationOrderPaymentAsync(order!, cancellationToken);
                    break;
                case PaymentStatus.Refused:
                    await orderUseCase.SetRefusalOrderPaymentAsync(order!, cancellationToken);
                    break;
                default:
                    throw new InvalidPaymentProcessingException();
            }
        }
    }
}
