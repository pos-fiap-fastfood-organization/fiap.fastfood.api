using Core.Controllers.Exceptions;
using Core.Controllers.Interfaces;
using Core.DTOs.Orders;
using Core.Entities;
using Core.Entities.Enums;
using Core.UseCases.Interfaces;

namespace Core.Controllers;

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
        var orderList = await _orderUseCase.GetAllByFilterAsync(filter, cancellationToken);

        var response = GetOrderResponse.Parse(orderList);

        return response;
    }

    public async Task<GetOrderResponse> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            throw new ArgumentException("Order ID cannot be null or empty.", nameof(id));
        }

        var order = await _orderUseCase.GetByIdAsync(id, cancellationToken);

        OrderNotFoundException.ThrowIfNullOrEmpty(id, order);

        var response = new GetOrderResponse(order!);

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
}