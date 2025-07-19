using Core.Entities.Enums;
using Infrastructure.Gateways.ApiClients.DTOs.MercadoPagoInputs;
using Infrastructure.Gateways.ApiClients.Interfaces;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Infrastructure.Gateways.ApiClients;

public class MercadoPagoClientGateway : IMercadoPagoClientGateway
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<MercadoPagoClientGateway> _logger;

    private static readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    public MercadoPagoClientGateway(HttpClient httpClient, ILogger<MercadoPagoClientGateway> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<MercadoPagoPaymentResponse> CreatePaymentAsync(
        MercadoPagoPaymentRequest mercadoPagoRequest,
        PaymentMethod paymentMethod,
        CancellationToken cancellationToken)
    {
        const string CREATE_PAYMENT_PATH_TEMPLATE = "/v1/payments";
        const string IDEMPOTENCY_KEY = "X-Idempotency-Key";

        var request = new HttpRequestMessage(HttpMethod.Post, CREATE_PAYMENT_PATH_TEMPLATE)
        {
            Headers = { { IDEMPOTENCY_KEY, mercadoPagoRequest.Metadata!.OrderNumber! } },
            Content = CreateContent(mercadoPagoRequest, paymentMethod)
        };

        var response = await _httpClient.SendAsync(request, cancellationToken);

        var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);

        if (response.IsSuccessStatusCode is false)
        {
            _logger.LogCritical(
                "Failed to create payment for order {OrderId}. Response: {ResponseContent}",
                mercadoPagoRequest.Metadata.OrderNumber,
                responseContent);

            throw new HttpRequestException($"Failed to create payment for order {mercadoPagoRequest.Metadata.OrderNumber}. Status code: {response.StatusCode}");
        }

        var mercadoPagoResponse = JsonSerializer.Deserialize<MercadoPagoPaymentResponse>(responseContent, _jsonSerializerOptions);

        return mercadoPagoResponse!;
    }

    private static StringContent CreateContent(MercadoPagoPaymentRequest requestContent, PaymentMethod paymentMethod)
    {
        const string CONTENT_TYPE = "application/json";

        return new StringContent(
            JsonSerializer.Serialize(requestContent),
            Encoding.UTF8,
            CONTENT_TYPE);
    }
}