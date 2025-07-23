using Core.Entities.Enums;

namespace Adapters.DTOs.Orders;

public record OrderItemRequest
(
    string? Id,
    string? Name,
    ItemCategory Category,
    decimal Price,
    int Amount
)
{ }