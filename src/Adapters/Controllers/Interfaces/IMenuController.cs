using Adapters.DTOs.Menus;

namespace Adapters.Controllers.Interfaces;

public interface IMenuController
{
    Task<MenuItemResponse> GetByIdAsync(string id, CancellationToken cancellationToken);

    Task<MenuItemResponse> RegisterAsync(RegisterMenuItemRequest request, CancellationToken cancellationToken);

    Task<IEnumerable<MenuItemResponse>> GetAllAsync(MenuItemFilter menuItemFilter, CancellationToken cancellationToken);

    Task SoftDeleteAsync(string id, CancellationToken cancellationToken);

    Task UpdateAsync(string id, UpdateMenuItemRequest request, CancellationToken cancellationToken);
}