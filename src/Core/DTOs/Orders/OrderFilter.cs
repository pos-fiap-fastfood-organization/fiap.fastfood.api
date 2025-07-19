using Core.Entities.Enums;

namespace Core.DTOs.Orders;

public record OrderFilter(OrderStatus? Status, int Page, int Size)
{
}