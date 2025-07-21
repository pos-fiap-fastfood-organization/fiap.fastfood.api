﻿using System.Text.Json.Serialization;

namespace Infrastructure.Gateways.ApiClients.DTOs.MercadoPagoInputs;

public record MercadoPagoTransactionData
{

    [JsonPropertyName("qr_code")]
    public string? QrCode { get; init; }

    [JsonPropertyName("qr_code_base64")]
    public string? QrCodeBase64 { get; init; }
}