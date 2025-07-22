using Core.Entities;
using Core.Entities.Enums;

namespace Core.Gateways.Interfaces;

public interface IPaymentGateway
{
    Task<OrderPayment> CreatePaymentAsync(Order order, Customer? customer, PaymentMethod paymentMethod, CancellationToken cancellationToken);
}