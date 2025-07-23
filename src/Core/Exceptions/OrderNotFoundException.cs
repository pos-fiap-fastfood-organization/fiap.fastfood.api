using Core.Entities;

namespace Core.Exceptions;

public class OrderNotFoundException : Exception
{
    private const string DEFAULT_MESSAGE = "Order with ID '{0}' not found.";

    internal OrderNotFoundException(string orderId)
        : base(string.Format(DEFAULT_MESSAGE, orderId))
    {
    }

    public static void ThrowIfNullOrEmpty(string orderId, Order? order)
    {
        if (order is null)
        {
            throw new OrderNotFoundException(orderId);
        }
    }
}