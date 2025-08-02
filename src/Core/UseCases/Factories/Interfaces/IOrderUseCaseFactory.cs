using Core.UseCases.Interfaces;

namespace Core.UseCases.Factories.Interfaces
{
    public interface IOrderUseCaseFactory
    {
        IOrderUseCase Create();
    }
}
