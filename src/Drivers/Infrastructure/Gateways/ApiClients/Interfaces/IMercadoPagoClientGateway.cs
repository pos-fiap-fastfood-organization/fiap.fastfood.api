using Core.Entities.Enums;
using Infrastructure.Gateways.ApiClients.DTOs.MercadoPagoInputs;

namespace Infrastructure.Gateways.ApiClients.Interfaces;

public interface IMercadoPagoClientGateway
{
    Task<MercadoPagoPaymentResponse> CreatePaymentAsync(
         MercadoPagoPaymentRequest mercadoPagoRequest,
         PaymentMethod paymentMethod,
         CancellationToken cancellationToken);
}