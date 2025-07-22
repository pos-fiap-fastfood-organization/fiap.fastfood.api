using Core.DTOs.Customers;
using Core.Entities;

namespace Core.UseCases.Interfaces;

public interface ICustomerUseCase
{
    Task<Customer> GetByCpfAsync(string cpf, CancellationToken cancellationToken);
    Task<Customer?> GetByIdAsync(string? id, CancellationToken cancellationToken);
    Task<Customer> InsertOneAsync(Customer customer, CancellationToken cancellationToken);
}