using Core.Entities.Enums;

namespace Adapters.DTOs.Menus;

public record MenuItemFilter(
    string? Name,
    MenuCategory? Category,
    int Skip,
    int Limit);