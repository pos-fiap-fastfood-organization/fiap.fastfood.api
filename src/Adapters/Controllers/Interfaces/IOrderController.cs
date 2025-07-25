using Adapters.Presenters.Orders;
using Core.Entities;

namespace Adapters.Controllers.Interfaces;

public interface IOrderController
{
    Task<Pagination<GetOrderResponse>> GetAllByFilterAsync(OrderFilter filter, CancellationToken cancellationToken);

    Task<GetOrderResponse> GetByIdAsync(string id, CancellationToken cancellationToken);

    Task<Order> CreateOrderAsync(CreateOrderRequest request, CancellationToken cancellationToken);

    Task<OrderCheckoutResponse> CheckoutAsync(string id, OrderCheckoutRequest request, CancellationToken cancellationToken);

    Task ConfirmPaymentAsync(string id, CancellationToken cancellationToken);

    Task DeleteAsync(string id, CancellationToken cancellationToken);

    Task<GetOrderResponse> UpdateStatusAsync(
        string id,
        UpdateStatusOrderRequest updateStatusRequest,
        CancellationToken cancellationToken);
    Task<GetOrderPaymentStatusResponse> GetPaymentStatusAsync(string id, CancellationToken cancellationToken);
    Task ProcessPaymentWebhookAsync(OrderPaymentWebhookRequest paymentWebhookRequest, CancellationToken cancellationToken);
}