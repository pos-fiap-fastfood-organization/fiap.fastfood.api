using Core.Entities;
using Core.Entities.Enums;
using Core.Gateways.Interfaces;
using Infrastructure.Gateways.ApiClients.DTOs.MercadoPagoInputs;
using Infrastructure.Gateways.ApiClients.Interfaces;

namespace Infrastructure.Gateways.ApiClients.Converters;

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