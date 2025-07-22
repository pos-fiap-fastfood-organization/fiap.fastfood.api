using Core.Entities.Enums;

namespace Core.UseCases.Exceptions;

internal class PaymentMethodNotSupportedException : Exception
{
    private const string DEFAULT_MESSAGE = "Payment method '{0}' not supported.";

    internal PaymentMethodNotSupportedException(PaymentMethod paymentMethod)
        : base(string.Format(DEFAULT_MESSAGE, paymentMethod.ToString()))
    {
    }

    internal static void ThrowIfPaymentMethodIsNotSupported(PaymentMethod paymentMethod)
    {
        if (paymentMethod != PaymentMethod.Pix)
            throw new PaymentMethodNotSupportedException(paymentMethod);
    }
}