using Core.Entities;
using System.Text.Json.Serialization;

namespace Infrastructure.Gateways.ApiClients.DTOs.MercadoPagoInputs;

public record MercadoPagoPaymentRequest
{
    [JsonPropertyName("transaction_amount")]
    public decimal TransactionAmount { get; init; }

    [JsonPropertyName("payment_method_id")]
    public string? PaymentMethodId { get; init; }

    [JsonPropertyName("metadata")]
    public MercadoPagoMetadata? Metadata { get; init; }

    [JsonPropertyName("payer")]
    public MercadoPagoPayer? Payer { get; init; }

    public MercadoPagoPaymentRequest(Order order, Customer? customer)
    {
        TransactionAmount = order.TotalPrice;
        Payer = new MercadoPagoPayer(customer);
        PaymentMethodId = order.PaymentMethod.ToString().ToLower();

        Metadata = new MercadoPagoMetadata
        {
            OrderNumber = order.Id
        };
    }
}