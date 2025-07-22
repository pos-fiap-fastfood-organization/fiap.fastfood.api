using Core.Entities.Exceptions.Abstractions;

namespace Core.Entities.Exceptions;

public class InvalidStockItemPropertyException(string message) : BaseEntityException<ItemQuantity>(message)
{
    public static void ThrowIfIsNullOrWhiteSpace(string? value, string propertyName, Type type)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidStockItemPropertyException($"The property {propertyName} from {type} can't be null or white space");
        }
    }

    public static void ThrowIfIsEqualOrLowerThanZero(int value, string propertyName, Type type)
    {
        if (value <= 0)
        {
            throw new InvalidStockItemPropertyException($"The property {propertyName} from {type} can't be equals or lower than zero");
        }
    }
}