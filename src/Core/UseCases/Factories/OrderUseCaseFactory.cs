using Core.UseCases.Factories.Interfaces;
using Core.UseCases.Interfaces;

namespace Core.UseCases.Factories
{
    public class OrderUseCaseFactory : IOrderUseCaseFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public OrderUseCaseFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IOrderUseCase Create()
        {
            var orderUseCase = (IOrderUseCase)_serviceProvider.GetService(typeof(IOrderUseCase))!;
            return orderUseCase;
        }
    }
}
