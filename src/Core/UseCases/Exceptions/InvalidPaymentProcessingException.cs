namespace Core.UseCases.Exceptions;

internal class InvalidPaymentProcessingException : Exception
{
    private const string DEFAULT_MESSAGE = "Payment status not valid for processing.";
    internal InvalidPaymentProcessingException()
        : base(DEFAULT_MESSAGE)
    {
    }
}