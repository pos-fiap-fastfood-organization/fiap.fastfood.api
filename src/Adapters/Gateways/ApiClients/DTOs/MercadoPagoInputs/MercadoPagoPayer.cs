using System.Text.Json.Serialization;

namespace Adapters.Gateways.ApiClients.DTOs.MercadoPagoInputs;

public record MercadoPagoPayer
{
    public MercadoPagoPayer()
    {
        string name = "fakeUserName";
        string email = "fakeemail@fake.com";

        Email = email;
        FirstName = name!.Split(' ').FirstOrDefault();
        LastName = name!.Split(' ').LastOrDefault();
    }

    [JsonPropertyName("first_name")]
    public string? FirstName { get; init; }

    [JsonPropertyName("last_name")]
    public string? LastName { get; init; }

    [JsonPropertyName("email")]
    public string? Email { get; init; }
}