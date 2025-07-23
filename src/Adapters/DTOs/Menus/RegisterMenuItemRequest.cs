using Core.Entities.Enums;
using System.Text.Json.Serialization;

namespace Adapters.DTOs.Menus;
public record RegisterMenuItemRequest(
    string? Name,
    decimal Price,
    MenuCategory Category,
    string? Description)
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public MenuCategory Category { get; init; } = Category;
}