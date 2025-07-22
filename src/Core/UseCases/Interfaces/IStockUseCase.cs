using Core.Entities;

namespace Core.UseCases.Interfaces;

public interface IStockUseCase
{
    void RegisterOrder(Order order, CancellationToken cancellation, DateTime? finishDate = null);
}