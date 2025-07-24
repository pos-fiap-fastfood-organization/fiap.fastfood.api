using Core.Entities;
using Core.Entities.Enums;

namespace Adapters.Presenters.Orders;

public record GetOrderPaymentStatusResponse
(
    string Id,
    OrderStatus Status,
    PaymentMethod PaymentMethod,
    decimal TotalPrice
)
{
    public GetOrderPaymentStatusResponse(Order order)
        : this(
            order.Id!,
            order.Status,
            order.PaymentMethod,
            order.TotalPrice)
    {
    }
}