using Core.Entities.Enums;

namespace Core.UseCases.Exceptions;

internal class InvalidOrderStatusException : Exception
{
    private const string DEFAULT_MESSAGE = "OrderStatus cannot be None.";
    internal InvalidOrderStatusException()
        : base(DEFAULT_MESSAGE)
    {
    }

    internal static void ThrowIfInvalidStatus(OrderStatus value)
    {
        if (value is OrderStatus.None)
        {
            throw new InvalidOrderStatusException();
        }
    }
}