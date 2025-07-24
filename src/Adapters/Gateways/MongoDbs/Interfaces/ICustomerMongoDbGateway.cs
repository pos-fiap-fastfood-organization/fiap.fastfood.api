using Adapters.Gateways.MongoDbs.Entities;

namespace Adapters.Gateways.MongoDbs.Interfaces;

public interface ICustomerMongoDbGateway
{
    Task<CustomerMongoDb?> GetByCpfAsync(string cpf, CancellationToken cancellationToken);
    Task<CustomerMongoDb?> GetByIdAsync(string id, CancellationToken cancellationToken);
    Task<CustomerMongoDb> InsertOneAsync(CustomerMongoDb customerMongoDb, CancellationToken cancellationToken);
}