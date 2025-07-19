using Core.DTOs.Menus;
using Infrastructure.Gateways.MongoDbs.Entities;

namespace Infrastructure.Gateways.MongoDbs.Interfaces;

public interface IMenuMongoDbGateway
{
    Task<MenuItemMongoDb?> GetByIdAsync(string id, CancellationToken cancellationToken);

    Task<MenuItemMongoDb> InsertOneAsync(MenuItemMongoDb menuItem, CancellationToken cancellationToken);

    Task<IEnumerable<MenuItemMongoDb>> GetAllAsync(MenuItemFilter menuItemFilter, CancellationToken cancellationToken);

    Task<bool> SoftDeleteAsync(string id, CancellationToken cancellationToken);

    Task UpdateAsync(string id, MenuItemMongoDb menuItem, CancellationToken cancellationToken);

    Task<MenuItemMongoDb?> GetByNameAsync(string name, CancellationToken cancellationToken);
}