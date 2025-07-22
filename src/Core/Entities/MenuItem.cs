using Core.Entities.Enums;
using Core.Entities.Exceptions;

namespace Core.Entities;

public class MenuItem
{
    private string? _name;
    private decimal _price;
    private string? _description;

    public string Id { get; set; } = string.Empty;
    public DateTime Created { get; set; }

    public string Name
    {
        get => _name!;
        set => _name = ValidateName(value);
    }

    public decimal Price
    {
        get => _price;
        set => _price = ValidatePrice(value);
    }

    public string Description
    {
        get => _description!;
        set => _description = ValidateDescription(value);
    }

    public bool IsActive { get; set; }
    public MenuCategory Category { get; set; }

    public MenuItem()
    {
        
    }
    public MenuItem(string? name, decimal price, MenuCategory category, string? description, bool isActive)
    {
        Name = name!;
        Price = price;
        Category = category;
        IsActive = isActive;
        Description = description!;
    }

    private string ValidateName(string? value)
    {
        MenuItemException.ThrowIfEmptyOrWhiteSpace(value, nameof(Name));
        return value!.Trim();
    }

    private decimal ValidatePrice(decimal value)
    {
        MenuItemException.ThrowIfZero(value, nameof(Price));
        MenuItemException.ThrowIfNegative(value, nameof(Price));
        return value;
    }

    private string ValidateDescription(string? value)
    {
        MenuItemException.ThrowIfEmptyOrWhiteSpace(value, nameof(Description));
        MenuItemException.ThrowIfExceedsMaxLength(value!, nameof(Description), 200);
        return value!;
    }
}