using Core.Entities;
using Core.Entities.Enums;

namespace Adapters.Presenters.Orders;

public record GetOrderResponse
(
    string Id,
    string CustomerId,
    string CustomerName,
    IEnumerable<OrderItemResponse> Items,
    OrderStatus Status,
    PaymentMethod PaymentMethod,
    string? Notes,
    decimal TotalPrice
)
{
    public GetOrderResponse(Order order)
        : this(
            order.Id!,
            order.CustomerId!,
            order.CustomerName!,
            OrderItemResponse.Parse(order.Items),
            order.Status,
            order.PaymentMethod,
            order.Notes,
            order.TotalPrice)
    {
    }

    public static GetOrderResponse Parse(Order order)
    {
        IEnumerable<OrderItemResponse> orderItemResponse = OrderItemResponse.Parse(order.Items);

        return new GetOrderResponse(
            order.Id!,
            order.CustomerId!,
            order.CustomerName!,
            orderItemResponse,
            order.Status,
            order.PaymentMethod,
            order.Notes,
            order.TotalPrice);
    }

    public static IEnumerable<GetOrderResponse> Parse(IEnumerable<Order> orders)
    {
        return orders.Select(Parse);
    }

    public static Pagination<GetOrderResponse> Parse(Pagination<Order> orderList)
    {
        return new Pagination<GetOrderResponse>()
        {
            Page = orderList.Page,
            Size = orderList.Size,
            TotalPages = orderList.TotalPages,
            TotalCount = orderList.TotalCount,
            Items = orderList.Items.Select(Parse)
        };
    }
}