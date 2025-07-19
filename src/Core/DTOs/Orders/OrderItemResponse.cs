using Core.Entities;
using Core.Entities.Enums;

namespace Core.DTOs.Orders;

public record OrderItemResponse
(
    string? Name,
    ItemCategory? Category,
    decimal Price,
    int Amount
)
{
    internal static OrderItemResponse Parse(OrderItem item)
    {
        return new OrderItemResponse(item.Name, item.Category, item.Price, item.Amount);
    }

    internal static IEnumerable<OrderItemResponse> Parse(IEnumerable<OrderItem> items)
    {
        return items.Select(Parse);
    }
}