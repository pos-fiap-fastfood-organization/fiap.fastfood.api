namespace Core.Entities;

public class OrderPayment
{
    public long Id { get; set; }
    public decimal Amount { get; set; }
    public string? QrCode { get; set; }
    public string? PaymentMethod { get; set; }
    public string? QrCodeBase64 { get; set; }
}