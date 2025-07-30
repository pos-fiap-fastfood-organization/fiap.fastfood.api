using Core.Entities.Enums;

namespace Adapters.Presenters.Orders;

public record OrderPaymentWebhookRequest(string OrderId, PaymentStatus PaymentStatus) { }