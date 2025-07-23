using Core.Entities;
using Core.Entities.Enums;
using Infrastructure.DataAccess.MongoAdapter.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Adapters.Gateways.MongoDbs.Entities;

[BsonIgnoreExtraElements]
[BsonDiscriminator("menuItem")]
public class MenuItemMongoDb : MongoEntity
{
    public string? Name { get; set; }
    public decimal Price { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }
    public string? Description { get; set; }

    [BsonRepresentation(BsonType.String)]
    public MenuCategory Category { get; set; }

    public MenuItemMongoDb(MenuItem menuItem)
    {
        Name = menuItem.Name;
        Price = menuItem.Price;
        Category = menuItem.Category;
        IsActive = menuItem.IsActive;
        Description = menuItem.Description;
    }

    public static IEnumerable<MenuItem> ToCore(IEnumerable<MenuItemMongoDb> menuItemMongoDbList)
    {
        return menuItemMongoDbList.Select(ToCore);
    }

    public static MenuItem ToCore(MenuItemMongoDb menuItemMongoDb)
    {
        return new MenuItem()
        {
            Id = menuItemMongoDb.Id,
            Name = menuItemMongoDb.Name!,
            Price = menuItemMongoDb.Price,
            Created = menuItemMongoDb.Created,
            IsActive = menuItemMongoDb.IsActive,
            Category = menuItemMongoDb.Category,
            Description = menuItemMongoDb.Description!
        };
    }

    public MenuItem ToCore()
    {
        return new MenuItem()
        {
            Id = Id,
            Name = Name!,
            Price = Price,
            Created = Created,
            IsActive = IsActive,
            Category = Category,
            Description = Description!
        };
    }
}