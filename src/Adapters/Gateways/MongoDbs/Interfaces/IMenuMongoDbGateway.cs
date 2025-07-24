using Adapters.Gateways.MongoDbs.Entities;
using Core.Entities.Enums;

namespace Adapters.Gateways.MongoDbs.Interfaces;

public interface IMenuMongoDbGateway
{
    Task<MenuItemMongoDb?> GetByIdAsync(string id, CancellationToken cancellationToken);

    Task<MenuItemMongoDb> InsertOneAsync(MenuItemMongoDb menuItem, CancellationToken cancellationToken);

    Task<bool> SoftDeleteAsync(string id, CancellationToken cancellationToken);

    Task UpdateAsync(string id, MenuItemMongoDb menuItem, CancellationToken cancellationToken);

    Task<MenuItemMongoDb?> GetByNameAsync(string name, CancellationToken cancellationToken);

    Task<IEnumerable<MenuItemMongoDb>> GetAllAsync(string? name, MenuCategory? category, int skip, int limit, CancellationToken cancellationToken);
}