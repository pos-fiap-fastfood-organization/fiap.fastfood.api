using Core.Gateways.Interfaces;
using Infrastructure.DataAccess.MongoAdapter.Connections;
using Infrastructure.DataAccess.MongoAdapter.Factories;
using Infrastructure.DataAccess.MongoAdapter.Interfaces;
using Infrastructure.Gateways.ApiClients;
using Infrastructure.Gateways.ApiClients.Converters;
using Infrastructure.Gateways.ApiClients.Interfaces;
using Infrastructure.Gateways.Loggers;
using Infrastructure.Gateways.MongoDbs;
using Infrastructure.Gateways.MongoDbs.Converters;
using Infrastructure.Gateways.MongoDbs.Interfaces;
using Polly;
using System.Net.Http.Headers;

namespace Api.Extensions;

public static class InfrastructureExtension
{
    private const string STRING_CONNECTION_MONGO = "StringConnectionMongo";

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

    public static IServiceCollection AddDatabases(this IServiceCollection services)
    {
        var stringConnectionMongo = Environment.GetEnvironmentVariable(STRING_CONNECTION_MONGO);

        services.AddSingleton<IMongoConnection>(new MongoConnection("default", stringConnectionMongo!, "FastFood.Api"));

        services.AddSingleton(DataContextFactory.Create);

        return services;
    }
}