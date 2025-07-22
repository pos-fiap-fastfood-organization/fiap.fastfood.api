using Core.Entities;

namespace Core.Gateways.Interfaces;

public interface ICustomerGateway
{
    Task<Customer?> GetByCpfAsync(string cpf, CancellationToken cancellationToken);
    Task<Customer?> GetByIdAsync(string id, CancellationToken cancellationToken);
    Task<Customer> InsertOneAsync(Customer customer, CancellationToken cancellationToken);
}