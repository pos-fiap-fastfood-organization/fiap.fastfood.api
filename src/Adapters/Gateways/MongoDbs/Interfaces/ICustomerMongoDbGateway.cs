using Adapters.Gateways.MongoDbs.Entities;

namespace Adapters.Gateways.MongoDbs.Interfaces;

public interface ICustomerMongoDbGateway
{
    Task<CustomerMongoDb?> GetByIdAsync(string id, CancellationToken cancellationToken);
}