using Core.Entities;
using Core.Entities.Enums;
using Core.Exceptions;
using Core.Gateways.Interfaces;
using Core.UseCases.Exceptions;
using Core.UseCases.Interfaces;

namespace Core.UseCases;

public class OrderUseCase : IOrderUseCase
{
    private const string PAYMENT_REFUSED_REASON = "Payment refused.";
    private readonly IOrderGateway _orderGateway;
    private readonly IPaymentUseCase _paymentUseCase;

    public OrderUseCase(IOrderGateway orderGateway, IPaymentUseCase paymentUseCase)
    {
        _orderGateway = orderGateway;
        _paymentUseCase = paymentUseCase;
    }

    public async Task<Order> GetValidatedOrderForCheckoutAsync(string id, PaymentMethod paymentType, CancellationToken cancellationToken)
    {
        var order = await GetByIdAsync(id, cancellationToken);
        ValidateOrderForCheckout(id, paymentType, order);

        return order!;
    }

    public async Task<Order> CheckoutAsync(Order order, PaymentMethod paymentMethod, CancellationToken cancellationToken)
    {
        ValidateOrderForCheckout(order.Id!, paymentMethod, order);

        order.PaymentMethod = paymentMethod;

        var orderPayment = await _paymentUseCase.CreateOrderPaymentAsync(order, cancellationToken);

        order.SetPayment(orderPayment);

        await _orderGateway.UpdatePaymentAsync(order!.Id!, order.PaymentMethod, order.Payment!, cancellationToken);

        return order;
    }

    public Task<Order> CreateOrderAsync(Order order, CancellationToken cancellationToken)
    {
        return _orderGateway.InsertOneAsync(order, cancellationToken);
    }

    public Task DeleteAsync(string id, CancellationToken cancellationToken)
    {
        return _orderGateway.DeleteAsync(id, cancellationToken);
    }

    public Task<Pagination<Order>> GetAllByFilterAsync(OrderStatus? status, int size, int page, CancellationToken cancellationToken)
    {

        OrderFilterException.ThrowIfInvalidPage(page);
        OrderFilterException.ThrowIfInvalidSize(size);

        return _orderGateway.GetAllByFilterAsync(status, size, page, cancellationToken);
    }

    public async Task<IEnumerable<Order>> GetAllPendingAsync(CancellationToken cancellationToken)
    {
        return await _orderGateway.GetAllPendingAsync(cancellationToken);
    }

    public Task<Order?> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        return _orderGateway.GetByIdAsync(id, cancellationToken);
    }

    public async Task<Order> UpdateStatusAsync(string id, OrderStatus status, CancellationToken cancellationToken)
    {
        InvalidOrderStatusException.ThrowIfInvalidStatus(status);

        return await _orderGateway.UpdateStatusAsync(id, status, cancellationToken);
    }

    public async Task SetConfirmationOrderPaymentAsync(Order order, CancellationToken cancellationToken)
    {
        order!.ConfirmPayment();

        await _orderGateway.UpdateStatusAsync(order.Id!, order.Status, cancellationToken);
    }

    public async Task SetRefusalOrderPaymentAsync(Order order, CancellationToken cancellationToken)
    {
        order!.Cancel(PAYMENT_REFUSED_REASON);

        await _orderGateway.UpdateStatusAsync(order.Id!, order.Status, order.Notes, cancellationToken);
    }

    private static void ValidateOrderForCheckout(string id, PaymentMethod paymentType, Order? order)
    {
        OrderNotFoundException.ThrowIfNullOrEmpty(id, order);
        PaymentMethodNotSupportedException.ThrowIfPaymentMethodIsNotSupported(paymentType);
    }
}