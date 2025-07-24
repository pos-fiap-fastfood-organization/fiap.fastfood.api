using Core.Entities.Enums;
using System.Text.Json.Serialization;

namespace Adapters.Presenters.Menus;

public record UpdateMenuItemRequest(
    string? Name,
    decimal Price,
    MenuCategory Category,
    string? Description,
    bool IsActive)
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public MenuCategory Category { get; init; } = Category;
}