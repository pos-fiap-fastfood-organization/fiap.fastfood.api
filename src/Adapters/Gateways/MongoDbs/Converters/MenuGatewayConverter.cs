using Adapters.Gateways.MongoDbs.Entities;
using Adapters.Gateways.MongoDbs.Interfaces;
using Core.Entities;
using Core.Entities.Enums;
using Core.Gateways.Interfaces;

namespace Adapters.Gateways.MongoDbs.Converters;

public class MenuGatewayConverter : IMenuGateway
{
    private readonly IMenuMongoDbGateway _mongoDbGateway;

    public MenuGatewayConverter(IMenuMongoDbGateway MongoDbGateway)
    {
        _mongoDbGateway = MongoDbGateway;
    }

    public async Task<IEnumerable<MenuItem>> GetAllAsync(
        string? name,
        MenuCategory? category,
        int skip,
        int limit,
        CancellationToken cancellationToken)
    {
        var menuItemMongoDbList = await _mongoDbGateway.GetAllAsync(
             name,
             category,
             skip,
             limit,
             cancellationToken);

        return MenuItemMongoDb.ToCore(menuItemMongoDbList);
    }

    public async Task<MenuItem?> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        var menuItemMongoDb = await _mongoDbGateway.GetByIdAsync(id, cancellationToken);

        return menuItemMongoDb?.ToCore();
    }

    public async Task<MenuItem?> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        var menuItemMongoDb = await _mongoDbGateway.GetByNameAsync(name, cancellationToken);

        return menuItemMongoDb?.ToCore();
    }

    public async Task<MenuItem> InsertOneAsync(MenuItem menuItem, CancellationToken cancellationToken)
    {
        var menuItemMongoDb = new MenuItemMongoDb(menuItem);

        var insertedMenuItemMongoDb = await _mongoDbGateway.InsertOneAsync(menuItemMongoDb, cancellationToken);

        return insertedMenuItemMongoDb.ToCore();
    }

    public Task<bool> SoftDeleteAsync(string id, CancellationToken cancellationToken)
    {
        return _mongoDbGateway.SoftDeleteAsync(id, cancellationToken);
    }

    public Task UpdateAsync(string id, MenuItem menuItem, CancellationToken cancellationToken)
    {
        var menuItemMongoDb = new MenuItemMongoDb(menuItem);

        return _mongoDbGateway.UpdateAsync(id, menuItemMongoDb, cancellationToken);
    }
}