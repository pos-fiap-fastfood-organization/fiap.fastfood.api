using Core.Entities.Enums;

namespace Core.DTOs.Orders;

public record OrderCheckoutRequest(PaymentMethod PaymentType) { }