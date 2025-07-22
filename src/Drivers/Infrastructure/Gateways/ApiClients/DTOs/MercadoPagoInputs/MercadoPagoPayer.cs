using Core.Entities;
using System.Text.Json.Serialization;

namespace Infrastructure.Gateways.ApiClients.DTOs.MercadoPagoInputs;

public record MercadoPagoPayer
{
    public MercadoPagoPayer(Customer? customer)
    {
        string name = customer?.Name ?? "fakeUserName";
        string email = customer?.Email ?? "fakeemail@fake.com";

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