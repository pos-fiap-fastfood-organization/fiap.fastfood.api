using System.Text.Json.Serialization;

namespace Infrastructure.Gateways.ApiClients.DTOs.MercadoPagoInputs;

public record MercadoPagoPointOfInteraction
{
    [JsonPropertyName("transaction_data")]
    public MercadoPagoTransactionData? TransactionData { get; init; }
}