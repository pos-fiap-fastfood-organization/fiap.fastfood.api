using Core.Entities.Enums;

namespace Adapters.DTOs.Orders;

public record OrderCheckoutRequest(PaymentMethod PaymentType) { }