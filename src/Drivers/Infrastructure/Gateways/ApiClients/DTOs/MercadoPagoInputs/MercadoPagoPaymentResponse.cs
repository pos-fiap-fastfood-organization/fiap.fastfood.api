using Core.Entities;

namespace Infrastructure.Gateways.ApiClients.DTOs.MercadoPagoInputs;

public class MercadoPagoPaymentResponse
{
    public required long Id { get; set; }
    public required string PaymentMethod { get; set; }
    public required string QrCode { get; set; }
    public required string QrCodeBase64 { get; set; }
    public required decimal Amount { get; set; }

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