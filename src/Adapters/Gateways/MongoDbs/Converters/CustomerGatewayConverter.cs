using Adapters.Gateways.MongoDbs.Interfaces;
using Core.Entities;
using Core.Gateways.Interfaces;

namespace Adapters.Gateways.MongoDbs.Converters;

public class CustomerGatewayConverter : ICustomerGateway
{
    private readonly ICustomerMongoDbGateway _repository;

    public CustomerGatewayConverter(ICustomerMongoDbGateway repository)
    {
        _repository = repository;
    }

    public async Task<Customer?> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        var customerMongoDb = await _repository.GetByIdAsync(id, cancellationToken);

        return customerMongoDb?.ToCore();
    }
}