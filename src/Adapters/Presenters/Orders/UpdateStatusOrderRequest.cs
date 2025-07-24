using Core.Entities.Enums;

namespace Adapters.Presenters.Orders;

public record UpdateStatusOrderRequest(OrderStatus Status) { }