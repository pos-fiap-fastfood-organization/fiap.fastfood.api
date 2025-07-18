using Infrastructure.DataAccess.MongoAdapter.Contexts;
using Infrastructure.DataAccess.MongoAdapter.Contexts.Interfaces;
using Infrastructure.DataAccess.MongoAdapter.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DataAccess.MongoAdapter.Factories;

public static class DataContextFactory
{
    public static IMongoContext Create(IServiceProvider serviceProvider)
    {
        var mongoConnections = serviceProvider.GetServices<IMongoConnection>();

        var mongoConnection = mongoConnections.Where(w => w.ClusterName == "default").FirstOrDefault();

        var mongoDatabase = mongoConnection!.Client.GetDatabase("fiap_fastfood");

        return new MongoContext("default", mongoDatabase);
    }
}