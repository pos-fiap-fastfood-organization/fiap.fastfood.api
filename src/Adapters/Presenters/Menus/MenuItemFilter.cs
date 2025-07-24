using Core.Entities.Enums;

namespace Adapters.Presenters.Menus;

public record MenuItemFilter(
    string? Name,
    MenuCategory? Category,
    int Skip,
    int Limit);