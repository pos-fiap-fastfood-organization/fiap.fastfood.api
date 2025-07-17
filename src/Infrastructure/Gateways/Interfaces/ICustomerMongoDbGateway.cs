using Infrastructure.Gateways.Entities;

namespace Infrastructure.Gateways.Interfaces;

public interface ICustomerMongoDbGateway
{
    Task<CustomerMongoDb?> GetByCpfAsync(string cpf, CancellationToken cancellationToken);
    Task<CustomerMongoDb?> GetByIdAsync(string id, CancellationToken cancellationToken);
    Task<CustomerMongoDb> InsertOneAsync(CustomerMongoDb customerMongoDb, CancellationToken cancellationToken);
}