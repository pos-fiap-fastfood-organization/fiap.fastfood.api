using Core.Entities.Exceptions.Abstractions;

namespace Core.Entities.Exceptions;

public class CustomerException : BaseEntityException<Customer>
{
    public CustomerException(string propertyName) : base(propertyName)
    {
    }
}