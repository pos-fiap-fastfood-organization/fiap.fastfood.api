
namespace Core.UseCases.Exceptions;

internal class OrderFilterException : Exception
{
    public OrderFilterException()
    {
    }

    public OrderFilterException(string? message) : base(message)
    {
    }

    internal static void ThrowIfInvalidPage(int page)
    {
        if (page < 1)
        {
            throw new OrderFilterException("Page number must be greater than or equal to 1.");
        }
    }

    internal static void ThrowIfInvalidSize(int size)
    {
        if (size < 1)
        {
            throw new OrderFilterException("Size number must be greater than or equal to 1.");
        }
    }
}