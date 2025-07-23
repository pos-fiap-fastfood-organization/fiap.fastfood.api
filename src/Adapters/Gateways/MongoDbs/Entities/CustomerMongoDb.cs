using Core.Entities;
using Infrastructure.DataAccess.MongoAdapter.Entities;
using MongoDB.Bson.Serialization.Attributes;

namespace Adapters.Gateways.MongoDbs.Entities;

[BsonIgnoreExtraElements]
[BsonDiscriminator("customer")]
public class CustomerMongoDb : MongoEntity
{
    public string? CPF { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }

    public static CustomerMongoDb Create(Customer customer)
    {
        return new CustomerMongoDb
        {
            CPF = customer.CPF,
            Name = customer.Name,
            Email = customer.Email
        };
    }

    public Customer ToCore()
    {
        return new Customer()
        {
            Id = Id,
            CPF = CPF,
            Name = Name,
            Email = Email,
            Created = Created,
        };
    }
}