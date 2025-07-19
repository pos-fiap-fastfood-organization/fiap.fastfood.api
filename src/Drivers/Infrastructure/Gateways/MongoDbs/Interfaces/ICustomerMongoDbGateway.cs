using Infrastructure.Gateways.MongoDbs.Entities;

namespace Infrastructure.Gateways.MongoDbs.Interfaces;

public interface ICustomerMongoDbGateway
{
    Task<CustomerMongoDb?> GetByCpfAsync(string cpf, CancellationToken cancellationToken);
    Task<CustomerMongoDb?> GetByIdAsync(string id, CancellationToken cancellationToken);
    Task<CustomerMongoDb> InsertOneAsync(CustomerMongoDb customerMongoDb, CancellationToken cancellationToken);
}