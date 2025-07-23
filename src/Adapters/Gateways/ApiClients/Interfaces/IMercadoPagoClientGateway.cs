using Adapters.Gateways.ApiClients.DTOs.MercadoPagoInputs;
using Core.Entities.Enums;

namespace Adapters.Gateways.ApiClients.Interfaces;

public interface IMercadoPagoClientGateway
{
    Task<MercadoPagoPaymentResponse> CreatePaymentAsync(
         MercadoPagoPaymentRequest mercadoPagoRequest,
         PaymentMethod paymentMethod,
         CancellationToken cancellationToken);
}