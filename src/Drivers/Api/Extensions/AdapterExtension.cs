using Adapters.Controllers;
using Adapters.Controllers.Interfaces;
using Adapters.Gateways.ApiClients;
using Adapters.Gateways.ApiClients.Converters;
using Adapters.Gateways.ApiClients.Interfaces;
using Adapters.Gateways.Loggers;
using Adapters.Gateways.MongoDbs;
using Adapters.Gateways.MongoDbs.Converters;
using Adapters.Gateways.MongoDbs.Interfaces;
using Core.Gateways.Interfaces;
using Polly;
using System.Net.Http.Headers;

namespace Api.Extensions;

public static class AdapterExtension
{
    public static IServiceCollection AddCoreControllers(this IServiceCollection services)
    {
        services
            .AddSingleton<ICustomerController, CustomerController>()
            .AddSingleton<IMenuController, MenuController>()
            .AddSingleton<IOrderController, OrderController>()
            .AddSingleton<IPaymentController, PaymentController>()
            ;

        return services;
    }

    public static IServiceCollection AddGateways(this IServiceCollection services)
    {
        AddMongoDbs(services);
        AddClientApis(services);
        AddLoggers(services);

        return services;
    }

    private static void AddLoggers(IServiceCollection services)
    {
        services
            .AddSingleton<IStockGateway, StockLoggerGateway>()
            ;
    }

    private static void AddClientApis(IServiceCollection services)
    {
        const int RETRY_COUNT = 3;

        var mercadoPagoApiUrl = Environment.GetEnvironmentVariable("MERCADOPAGO_API_URL");
        var mercadoPagoApiToken = Environment.GetEnvironmentVariable("MERCADOPAGO_API_TOKEN");

        services
            .AddSingleton<IPaymentGateway, MercadoPagoGatewayConverter>()
            ;

        services.AddHttpClient<IMercadoPagoClientGateway, MercadoPagoClientGateway>(client =>
        {
            client.BaseAddress = new Uri(mercadoPagoApiUrl!);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", mercadoPagoApiToken);
        })
        .AddTransientHttpErrorPolicy(policyBuilder =>
        {
            return policyBuilder.WaitAndRetryAsync(
                RETRY_COUNT,
                attempt => TimeSpan.FromSeconds(0.4 * Math.Pow(2, attempt)));
        });
    }

    private static void AddMongoDbs(IServiceCollection services)
    {
        services
            .AddSingleton<ICustomerGateway, CustomerGatewayConverter>()
            .AddSingleton<IMenuGateway, MenuGatewayConverter>()
            .AddSingleton<IOrderGateway, OrderGatewayConverter>()
            ;

        services
            .AddSingleton<ICustomerMongoDbGateway, CustomerMongoDbGateway>()
            .AddSingleton<IMenuMongoDbGateway, MenuMongoDbGateway>()
            .AddSingleton<IOrderMongoDbGateway, OrderMongoDbGateway>()
            ;
    }
}