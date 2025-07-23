using Adapters.Controllers.Interfaces;
using Adapters.DTOs.Menus;
using Core.Entities;
using Core.UseCases.Interfaces;

namespace Adapters.Controllers;

public class MenuController : IMenuController
{
    private readonly IMenuUseCase _menuUseCase;

    public MenuController(IMenuUseCase menuUseCase)
    {
        _menuUseCase = menuUseCase;
    }

    public async Task<IEnumerable<MenuItemResponse>> GetAllAsync(MenuItemFilter menuItemFilter, CancellationToken cancellationToken)
    {
       IEnumerable<MenuItem> menuItems = await _menuUseCase.GetAllAsync(
            menuItemFilter.Name,
            menuItemFilter.Category,
            menuItemFilter.Skip,
            menuItemFilter.Limit,
            cancellationToken);

        var response = menuItems.Select(menuItem => new MenuItemResponse(
                            menuItem.Id,
                            menuItem.Name,
                            menuItem.Price,
                            menuItem.Category,
                            menuItem.Description,
                            menuItem.IsActive));

        return response;
    }

    public async Task<MenuItemResponse> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            throw new ArgumentNullException(nameof(id));
        }

        var menuItem = await _menuUseCase.GetByIdAsync(id, cancellationToken);

        if (menuItem is null)
        {
            throw new KeyNotFoundException($"Menu item with ID {id} not found.");
        }

        var response = new MenuItemResponse(
                            menuItem.Id,
                            menuItem.Name!,
                            menuItem.Price,
                            menuItem.Category,
                            menuItem.Description!,
                            menuItem.IsActive);

        return response;
    }

    public async Task<MenuItemResponse> RegisterAsync(RegisterMenuItemRequest request, CancellationToken cancellationToken)
    {
        var menuItem = new MenuItem(
            name: request.Name!,
            price: request.Price,
            category: request.Category,
            description: request.Description!,
            isActive: true);

        menuItem = await _menuUseCase.InsertOneAsync(menuItem, cancellationToken);

        var response = new MenuItemResponse(
            Id: menuItem.Id,
            Name: menuItem.Name!,
            Price: menuItem.Price,
            Category: menuItem.Category,
            Description: menuItem.Description!,
            IsActive: menuItem.IsActive);

        return response;
    }

    public async Task SoftDeleteAsync(string id, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            throw new ArgumentNullException(nameof(id));
        }

        var updateWasSuccessful = await _menuUseCase.SoftDeleteAsync(id, cancellationToken);

        if (updateWasSuccessful is false)
        {
            throw new KeyNotFoundException($"Menu item with ID {id} not found.");
        }
    }

    public async Task UpdateAsync(string id, UpdateMenuItemRequest request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            throw new ArgumentNullException(nameof(id));
        }

        var menuItemToUpdate = new MenuItem(request.Name, request.Price, request.Category, request.Description, request.IsActive);

        await _menuUseCase.UpdateAsync(id, menuItemToUpdate, cancellationToken);
    }
}