using System.Text.Json.Serialization;

namespace Adapters.Gateways.ApiClients.DTOs.MercadoPagoInputs;

public record MercadoPagoPointOfInteraction
{
    [JsonPropertyName("transaction_data")]
    public MercadoPagoTransactionData? TransactionData { get; init; }
}