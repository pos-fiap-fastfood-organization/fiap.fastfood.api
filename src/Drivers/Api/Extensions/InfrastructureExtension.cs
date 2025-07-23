using Infrastructure.DataAccess.MongoAdapter.Connections;
using Infrastructure.DataAccess.MongoAdapter.Factories;
using Infrastructure.DataAccess.MongoAdapter.Interfaces;

namespace Api.Extensions;

public static class InfrastructureExtension
{
    private const string STRING_CONNECTION_MONGO = "StringConnectionMongo";

    public static IServiceCollection AddDatabases(this IServiceCollection services)
    {
        var stringConnectionMongo = Environment.GetEnvironmentVariable(STRING_CONNECTION_MONGO);

        services.AddSingleton<IMongoConnection>(new MongoConnection("default", stringConnectionMongo!, "FastFood.Api"));

        services.AddSingleton(DataContextFactory.Create);

        return services;
    }
}