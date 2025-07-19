using Core.Entities.Enums;

namespace Core.DTOs.Orders;

public record OrderItemRequest
(
    string? Id,
    string? Name,
    ItemCategory Category,
    decimal Price,
    int Amount
)
{ }