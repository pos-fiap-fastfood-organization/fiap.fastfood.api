using Core.Entities;
using Core.Entities.Enums;

namespace Core.UseCases.Interfaces
{
    public interface IPaymentUseCase
    {
        Task<OrderPayment> CreateOrderPaymentAsync(Order order, CancellationToken cancellationToken);
        Task ProcessPaymentAsync(string id, PaymentStatus paymentStatus, CancellationToken cancellationToken);
    }
}
