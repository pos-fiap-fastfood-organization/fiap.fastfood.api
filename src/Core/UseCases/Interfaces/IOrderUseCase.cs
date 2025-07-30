using Core.Entities;
using Core.Entities.Enums;

namespace Core.UseCases.Interfaces;

public interface IOrderUseCase
{
    Task<Order> CheckoutAsync(Order order, Customer? customer, PaymentMethod paymentMethod, CancellationToken cancellationToken);

    Task<Order> CreateOrderAsync(Order order, CancellationToken cancellationToken);

    Task DeleteAsync(string id, CancellationToken cancellationToken);

    Task<Pagination<Order>> GetAllByFilterAsync(
        OrderStatus? status,
        int size,
        int page,
        CancellationToken cancellationToken);

    Task<IEnumerable<Order>> GetAllPendingAsync(CancellationToken cancellationToken);

    Task<Order?> GetByIdAsync(string id, CancellationToken cancellationToken);

    Task<Order> GetValidatedOrderForCheckoutAsync(string id, PaymentMethod paymentMethod, CancellationToken cancellationToken);
    Task ProcessPaymentAsync(string orderId, PaymentStatus paymentStatus, CancellationToken cancellationToken);
    Task<Order> UpdateStatusAsync(string id, OrderStatus status, CancellationToken cancellationToken);
}