using Core.Entities;

namespace Adapters.Presenters.Orders;

public class OrderCheckoutResponse
{
    public long Id { get; init; }
    public decimal Amount { get; init; }
    public string? QrCode { get; init; }
    public string? PaymentMethod { get; init; }
    public string? QrCodeBase64 { get; init; }

    public OrderCheckoutResponse(OrderPayment payment)
    {
        Id = payment.Id;
        Amount = payment.Amount;
        QrCode = payment.QrCode;
        QrCodeBase64 = payment.QrCodeBase64;
        PaymentMethod = payment.PaymentMethod;
    }
}