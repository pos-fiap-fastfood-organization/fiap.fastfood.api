using Core.Entities;
using Infrastructure.DataAccess.MongoAdapter.Entities;

namespace Infrastructure.Gateways.Entities;

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
            Id = this.Id,
            CPF = this.CPF,
            Name = this.Name,
            Email = this.Email,
            Created = this.Created,
        };
    }
}