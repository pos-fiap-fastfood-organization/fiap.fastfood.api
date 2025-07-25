using Core.Entities.Enums;

namespace Core.UseCases.Exceptions;

internal class InvalidPaymentStatusException : Exception
{
    private const string DEFAULT_MESSAGE = "Payment status cannot be None.";
    internal InvalidPaymentStatusException()
        : base(DEFAULT_MESSAGE)
    {
    }

    internal static void ThrowIfInvalidStatus(PaymentStatus value)
    {
        if (value is PaymentStatus.None)
        {
            throw new InvalidPaymentStatusException();
        }
    }
}