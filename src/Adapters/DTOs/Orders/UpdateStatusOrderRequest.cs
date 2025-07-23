using Core.Entities.Enums;

namespace Adapters.DTOs.Orders;

public record UpdateStatusOrderRequest(OrderStatus Status) { }