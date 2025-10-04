using Core.Entities;
using System.Text.Json.Serialization;

namespace Adapters.Gateways.ApiClients.DTOs.MercadoPagoInputs;

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

    [JsonPropertyName("notification_url")]
    public string NotificationUrl { get; set; }

    public MercadoPagoPaymentRequest(Order order)
    {
        TransactionAmount = order.TotalPrice;
        Payer = new MercadoPagoPayer();
        NotificationUrl = "https://webhook-test.com/?address=ce597306d08801680062ad913e9d50cb";
        PaymentMethodId = order.PaymentMethod.ToString().ToLower();

        Metadata = new MercadoPagoMetadata
        {
            OrderNumber = order.Id
        };
    }
}