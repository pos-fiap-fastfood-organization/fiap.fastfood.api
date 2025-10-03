using Core.Entities;
using Core.Gateways.Interfaces;
using Core.UseCases.Interfaces;

namespace Core.UseCases;

public class CustomerUseCase : ICustomerUseCase
{
    private readonly ICustomerGateway _customerGateway;

    public CustomerUseCase(ICustomerGateway customerGateway)
    {
        _customerGateway = customerGateway;
    }

    public async Task<Customer?> GetByIdAsync(string? id, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            return default;
        }

        var customer = await _customerGateway.GetByIdAsync(id, cancellationToken);

        return customer;
    }
}