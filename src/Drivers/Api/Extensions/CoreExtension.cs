using Adapters.Controllers.Interfaces;
using Core.UseCases;
using Core.UseCases.Interfaces;

namespace Api.Extensions;

public static class CoreExtension
{
    public static IServiceCollection AddUserCases(this IServiceCollection services)
    {
        services
            .AddSingleton<ICustomerUseCase, CustomerUseCase>()
            .AddSingleton<IMenuUseCase, MenuUseCase>()
            .AddSingleton<IOrderUseCase, OrderUseCase>()
            .AddSingleton<IPaymentUseCase, PaymentUseCase>()
            .AddSingleton<IStockUseCase, StockUseCase>()
            ;

        return services;
    }
}