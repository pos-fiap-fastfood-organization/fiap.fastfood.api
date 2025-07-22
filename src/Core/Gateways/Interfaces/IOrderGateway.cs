using Core.DTOs.Orders;
using Core.Entities;
using Core.Entities.Enums;

namespace Core.Gateways.Interfaces;

public interface IOrderGateway
{
    Task<Pagination<Order>> GetAllByFilterAsync(OrderFilter filter, CancellationToken cancellationToken);

    Task<Order?> GetByIdAsync(string id, CancellationToken cancellationToken);

    Task<Order> InsertOneAsync(Order order, CancellationToken cancellationToken);

    Task DeleteAsync(string id, CancellationToken cancellationToken);

    Task UpdatePaymentAsync(
        string id,
        PaymentMethod paymentMethod,
        OrderPayment orderPayment,
        CancellationToken cancellationToken);

    Task<Order> UpdateStatusAsync(
        string id,
        OrderStatus status,
        CancellationToken cancellationToken);
}