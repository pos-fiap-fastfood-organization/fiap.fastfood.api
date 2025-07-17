using Core.DTOs.Customers;

namespace Core.Controllers.Interfaces;

public interface ICustomerController
{
    Task<CustomerResponse> GetByCpfAsync(string cpf, CancellationToken cancellationToken);
    Task<CustomerResponse> RegisterAsync(RegisterCustomerRequest request, CancellationToken cancellationToken);
}