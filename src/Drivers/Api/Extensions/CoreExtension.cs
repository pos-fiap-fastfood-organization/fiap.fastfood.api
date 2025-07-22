using Core.Controllers;
using Core.Controllers.Interfaces;
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
            .AddSingleton<IStockUseCase, StockUseCase>()
            ;

        return services;
    }

    public static IServiceCollection AddCoreControllers(this IServiceCollection services)
    {
        services
            .AddSingleton<ICustomerController, CustomerController>()
            .AddSingleton<IMenuController, MenuController>()
            .AddSingleton<IOrderController, OrderController>()
            ;

        return services;
    }
}