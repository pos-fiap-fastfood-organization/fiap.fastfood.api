using System.Text.Json.Serialization;

namespace Adapters.Gateways.ApiClients.DTOs.MercadoPagoInputs;

public record MercadoPagoMetadata
{
    [JsonPropertyName("order_number")]
    public string? OrderNumber { get; init; }
}