using Core.Entities.Enums;

namespace Adapters.Presenters.Orders;

public record OrderCheckoutRequest(PaymentMethod PaymentType) { }