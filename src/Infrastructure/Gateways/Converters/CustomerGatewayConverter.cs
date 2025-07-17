using Core.Entities;
using Core.Gateways.Interfaces;
using Infrastructure.Gateways.Entities;
using Infrastructure.Gateways.Interfaces;

namespace Infrastructure.Gateways.Converters;

public class CustomerGatewayConverter : ICustomerGateway
{
    private readonly ICustomerMongoDbGateway _repository;

    public CustomerGatewayConverter(ICustomerMongoDbGateway repository)
    {
        _repository = repository;
    }

    public async Task<Customer?> GetByCpfAsync(string cpf, CancellationToken cancellationToken)
    {
        var customerMongoDb = await _repository.GetByCpfAsync(cpf, cancellationToken);

        return customerMongoDb?.ToCore();
    }

    public async Task<Customer?> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        var customerMongoDb = await _repository.GetByIdAsync(id, cancellationToken);

        return customerMongoDb?.ToCore();
    }

    public async Task<Customer> InsertOneAsync(Customer customer, CancellationToken cancellationToken)
    {
        var customerMongoDb = CustomerMongoDb.Create(customer);

        var customerMongoDbInserted = await _repository.InsertOneAsync(customerMongoDb, cancellationToken);

        return customerMongoDbInserted.ToCore();
    }
}