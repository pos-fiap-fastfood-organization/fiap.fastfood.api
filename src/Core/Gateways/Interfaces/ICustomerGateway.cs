using Core.Entities;

namespace Core.Gateways.Interfaces;

public interface ICustomerGateway
{
    Task<Customer?> GetByIdAsync(string id, CancellationToken cancellationToken);
}