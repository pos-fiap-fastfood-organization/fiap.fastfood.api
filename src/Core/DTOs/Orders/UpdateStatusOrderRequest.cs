using Core.Entities.Enums;

namespace Core.DTOs.Orders;

public record UpdateStatusOrderRequest(OrderStatus Status) { }