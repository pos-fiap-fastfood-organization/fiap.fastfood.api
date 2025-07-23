using Core.Entities;

namespace Adapters.Gateways.MongoDbs.Entities;

public class OrderPaymentMongoDb
{
    public long Id { get; set; }
    public string? QrCode { get; set; }
    public decimal Amount { get; set; }
    public string? QrCodeBase64 { get; set; }
    public string? PaymentMethod { get; set; }

    public OrderPaymentMongoDb(OrderPayment orderPayment)
    {
        Id = orderPayment.Id;
        QrCode = orderPayment.QrCode;
        Amount = orderPayment.Amount;
        QrCodeBase64 = orderPayment.QrCodeBase64;
        PaymentMethod = orderPayment.PaymentMethod;
    }

    internal OrderPayment ToCore()
    {
        return new OrderPayment()
        {
            Id = Id,
            Amount = Amount,
            QrCode = QrCode,
            QrCodeBase64 = QrCodeBase64,
            PaymentMethod = PaymentMethod,
        };
    }
}