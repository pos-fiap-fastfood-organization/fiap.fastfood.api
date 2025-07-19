using Core.Entities;

namespace Core.DTOs.Orders;

public record CreateOrderRequest
(
    string? CustomerId,
    string? CustomerName,
    IEnumerable<OrderItemRequest> Items
)
{
    internal IEnumerable<OrderItem> ParseOrderItem()
    {
        return Items
            .Select(item =>
            {
                return new OrderItem
                (
                    item.Id,
                    item.Name,
                    item.Category,
                    item.Price,
                    item.Amount
                );
            });

    }
}