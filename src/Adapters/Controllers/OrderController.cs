using Adapters.Controllers.Interfaces;
using Adapters.Presenters.Orders;
using Core.Entities;
using Core.Entities.Enums;
using Core.Exceptions;
using Core.UseCases.Interfaces;

namespace Adapters.Controllers;

public class OrderController : IOrderController
{
    private readonly IStockUseCase _stockUseCase;
    private readonly IOrderUseCase _orderUseCase;
    private readonly ICustomerUseCase _customerUseCase;

    public OrderController(
        ICustomerUseCase customerUseCase,
        IStockUseCase stockUseCase,
        IOrderUseCase orderUseCase)
    {
        _orderUseCase = orderUseCase;
        _customerUseCase = customerUseCase;
        _stockUseCase = stockUseCase;
    }

    public async Task<Pagination<GetOrderResponse>> GetAllByFilterAsync(OrderFilter filter, CancellationToken cancellationToken)
    {
        var orderList = await _orderUseCase.GetAllByFilterAsync(
            filter.Status, 
            filter.Size, 
            filter.Page, 
            cancellationToken);

        var response = GetOrderResponse.Parse(orderList);

        return response!;
    }

    public async Task<GetOrderResponse> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        Order? order = await GetAndValidateAsync(id, cancellationToken);

        var response = new GetOrderResponse(order!);

        return response;
    }

    public async Task<GetOrderPaymentStatusResponse> GetPaymentStatusAsync(string id, CancellationToken cancellationToken)
    {
        var order = await GetAndValidateAsync(id, cancellationToken);

        var response = new GetOrderPaymentStatusResponse(order!);

        return response;
    }

    public async Task<Order> CreateOrderAsync(CreateOrderRequest request, CancellationToken cancellationToken)
    {
        var items = request.ParseOrderItem();

        var order = new Order(request.CustomerId, request.CustomerName, items);

        var insertedOrder = await _orderUseCase.CreateOrderAsync(order, cancellationToken);

        return insertedOrder;
    }

    public async Task<GetOrderResponse> UpdateStatusAsync(string id, UpdateStatusOrderRequest updateStatusRequest, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            throw new ArgumentException("Order ID cannot be null or empty.", nameof(id));
        }

        var order = await _orderUseCase.UpdateStatusAsync(id, updateStatusRequest.Status, cancellationToken);

        _stockUseCase.RegisterOrder(order, cancellationToken);

        var response = new GetOrderResponse(order);

        return response;
    }

    public async Task DeleteAsync(string id, CancellationToken cancellationToken)
    {
        await _orderUseCase.DeleteAsync(id, cancellationToken);
    }

    public async Task<OrderCheckoutResponse> CheckoutAsync(string id, OrderCheckoutRequest request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            throw new ArgumentException("Order ID cannot be null or empty.", nameof(id));
        }

        var order = await _orderUseCase.GetValidatedOrderForCheckoutAsync(id, request.PaymentType, cancellationToken);
        var customer = await _customerUseCase.GetByIdAsync(order.CustomerId, cancellationToken);

        var checkoutOrder = await _orderUseCase.CheckoutAsync(order, customer, request.PaymentType, cancellationToken);

        var response = new OrderCheckoutResponse(checkoutOrder.Payment!);

        return response;
    }

    public Task ConfirmPaymentAsync(string id, CancellationToken cancellationToken)
    {
        return _orderUseCase.UpdateStatusAsync(id, OrderStatus.Received, cancellationToken);
    }

    private async Task<Order?> GetAndValidateAsync(string id, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            throw new ArgumentException("Order ID cannot be null or empty.", nameof(id));
        }

        var order = await _orderUseCase.GetByIdAsync(id, cancellationToken);

        OrderNotFoundException.ThrowIfNullOrEmpty(id, order);
        return order;
    }
}