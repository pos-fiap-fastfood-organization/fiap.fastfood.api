using Core.Entities;
using Core.Entities.Enums;
using Core.Exceptions;
using Core.Gateways.Interfaces;
using Core.UseCases.Interfaces;

namespace Core.UseCases;

public class MenuUseCase : IMenuUseCase
{
    private readonly IMenuGateway _menuGateway;

    public MenuUseCase(IMenuGateway menuGateway)
    {
        _menuGateway = menuGateway;
    }

    public Task<IEnumerable<MenuItem>> GetAllAsync(
        string? name,
        MenuCategory? category,
        int skip,
        int limit,
        CancellationToken cancellationToken)
    {
        return _menuGateway.GetAllAsync(
            name,
            category,
            skip,
            limit,
            cancellationToken);
    }

    public Task<MenuItem?> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        return _menuGateway.GetByIdAsync(id, cancellationToken);
    }

    public Task<MenuItem?> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        return _menuGateway.GetByNameAsync(name, cancellationToken);
    }

    public Task<MenuItem> InsertOneAsync(MenuItem menuItem, CancellationToken cancellationToken)
    {
        return _menuGateway.InsertOneAsync(menuItem, cancellationToken);
    }

    public Task<bool> SoftDeleteAsync(string id, CancellationToken cancellationToken)
    {
        return _menuGateway.SoftDeleteAsync(id, cancellationToken);
    }

    public async Task UpdateAsync(string id, MenuItem menuItemToUpdate, CancellationToken cancellationToken)
    {
        var menuItem = await _menuGateway.GetByIdAsync(id, cancellationToken);

        if (menuItem is null)
        {
            throw new KeyNotFoundException($"Menu item with ID {id} not found.");
        }

        menuItem.Name = menuItemToUpdate.Name!;
        menuItem.Price = menuItemToUpdate.Price;
        menuItem.Category = menuItemToUpdate.Category;
        menuItem.Description = menuItemToUpdate.Description!;
        menuItem.IsActive = menuItemToUpdate.IsActive;

        var existingMenuItem = await _menuGateway.GetByNameAsync(menuItem.Name, cancellationToken);

        if (existingMenuItem is not null && existingMenuItem.Id != id)
        {
            throw new DuplicateItemException($"A menu item with the name '{menuItem.Name}' already exists.");
        }

        await _menuGateway.UpdateAsync(id, menuItem, cancellationToken);
    }
}