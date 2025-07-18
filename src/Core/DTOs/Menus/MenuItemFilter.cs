using Core.Entities.Enums;

namespace Core.DTOs.Menus;

public record MenuItemFilter(
    string? Name,
    MenuCategory? Category,
    int Skip,
    int Limit);