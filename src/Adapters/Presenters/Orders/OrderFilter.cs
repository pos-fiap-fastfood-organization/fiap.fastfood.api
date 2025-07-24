using Core.Entities.Enums;

namespace Adapters.Presenters.Orders;

public record OrderFilter(OrderStatus? Status, int Page, int Size)
{
}