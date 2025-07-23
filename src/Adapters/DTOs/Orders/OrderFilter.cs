using Core.Entities.Enums;

namespace Adapters.DTOs.Orders;

public record OrderFilter(OrderStatus? Status, int Page, int Size)
{
}