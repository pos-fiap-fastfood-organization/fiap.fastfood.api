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
    private readonly IPaymentGateway _paymentGateway;

    public OrderUseCase(
        IOrderGateway orderGateway,
        IPaymentGateway paymentGateway)
    {
        _orderGateway = orderGateway;
        _paymentGateway = paymentGateway;
    }

    public async Task<Order> GetValidatedOrderForCheckoutAsync(string id, PaymentMethod paymentType, CancellationToken cancellationToken)
    {
        var order = await GetByIdAsync(id, cancellationToken);
        ValidateOrderForCheckout(id, paymentType, order);

        return order!;
    }

    public async Task<Order> CheckoutAsync(Order order, Customer? customer, PaymentMethod paymentMethod, CancellationToken cancellationToken)
    {
        ValidateOrderForCheckout(order.Id!, paymentMethod, order);

        order.PaymentMethod = paymentMethod;

        var orderPayment = await CreateOrderPaymentAsync(order, customer, cancellationToken);

        order.SetPayment(orderPayment);

        await _orderGateway.UpdatePaymentAsync(order!.Id!, order.PaymentMethod, order.Payment!, cancellationToken);

        return order;
    }

    public Task<OrderPayment> CreateOrderPaymentAsync(Order order, Customer? customer, CancellationToken cancellationToken)
    {
        return _paymentGateway.CreatePaymentAsync(
            order,
            customer,
            order.PaymentMethod,
            cancellationToken);
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

    public async Task ProcessPaymentAsync(string id, PaymentStatus paymentStatus, CancellationToken cancellationToken)
    {
        InvalidPaymentStatusException.ThrowIfInvalidStatus(paymentStatus);

        var order = await GetByIdAsync(id, cancellationToken);
        OrderNotFoundException.ThrowIfNullOrEmpty(id, order);

        switch (paymentStatus)
        {
            case PaymentStatus.Approved:
                await ConfirmPaymentAsync(id, cancellationToken);
                break;
            case PaymentStatus.Refused:
                await SetPaymentRefusalAsync(id, cancellationToken);
                break;
            default:
                throw new InvalidPaymentProcessingException();
        }
    }

    private async Task<Order> ConfirmPaymentAsync(string id, CancellationToken cancellationToken)
    {
        var order = await GetByIdAsync(id, cancellationToken);
        OrderNotFoundException.ThrowIfNullOrEmpty(id, order);

        order!.ConfirmPayment();

        return await _orderGateway.UpdateStatusAsync(id, order.Status, cancellationToken);
    }


    private async Task<Order> SetPaymentRefusalAsync(string id, CancellationToken cancellationToken)
    {
        var order = await GetByIdAsync(id, cancellationToken);
        OrderNotFoundException.ThrowIfNullOrEmpty(id, order);

        order!.Cancel(PAYMENT_REFUSED_REASON);

        return await _orderGateway.UpdateStatusAsync(id, order.Status, order.Notes, cancellationToken);
    }

    private static void ValidateOrderForCheckout(string id, PaymentMethod paymentType, Order? order)
    {
        OrderNotFoundException.ThrowIfNullOrEmpty(id, order);
        PaymentMethodNotSupportedException.ThrowIfPaymentMethodIsNotSupported(paymentType);
    }
}