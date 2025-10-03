using Core.Entities;

namespace Core.UseCases.Interfaces;

public interface ICustomerUseCase
{
    Task<Customer?> GetByIdAsync(string? id, CancellationToken cancellationToken);
}