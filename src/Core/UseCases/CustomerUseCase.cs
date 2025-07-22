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

    public async Task<Customer> GetByCpfAsync(string cpf, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(cpf))
        {
            throw new ArgumentNullException(nameof(cpf));
        }

        var customer = await _customerGateway.GetByCpfAsync(cpf, cancellationToken);

        if (customer is null)
        {
            throw new ApplicationException("customer does not exist");
        }

        return customer;
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

    public async Task<Customer> InsertOneAsync(Customer newCustomer, CancellationToken cancellationToken)
    {
        var customer = await _customerGateway.GetByCpfAsync(newCustomer.CPF!, cancellationToken);

        if (customer is not null)
        {
            throw new ApplicationException("customer already exist");
        }

        var insertedCustomer = await _customerGateway.InsertOneAsync(newCustomer, cancellationToken);

        return insertedCustomer;
    }
}