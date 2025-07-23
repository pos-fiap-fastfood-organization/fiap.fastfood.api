using Adapters.Gateways.ApiClients.DTOs.MercadoPagoInputs;
using Adapters.Gateways.ApiClients.Interfaces;
using Core.Entities;
using Core.Entities.Enums;
using Core.Gateways.Interfaces;

namespace Adapters.Gateways.ApiClients.Converters;

public class MercadoPagoGatewayConverter : IPaymentGateway
{
    private readonly IMercadoPagoClientGateway _mercadoPagoClientGateway;

    public MercadoPagoGatewayConverter(IMercadoPagoClientGateway mercadoPagoClientGateway)
    {
        _mercadoPagoClientGateway = mercadoPagoClientGateway;
    }

    public async Task<OrderPayment> CreatePaymentAsync(Order order, Customer? customer, PaymentMethod paymentMethod, CancellationToken cancellationToken)
    {
        var paymentRequest = new MercadoPagoPaymentRequest(order, customer);

        var paymentResponse = await _mercadoPagoClientGateway.CreatePaymentAsync(paymentRequest, paymentMethod, cancellationToken);

        return paymentResponse.ToCore();
    }
}