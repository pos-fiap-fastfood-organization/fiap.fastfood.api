namespace Core.Entities.Enums;

public static class OrderStatusExtensions
{
    public static PaymentStatus ToPaymentStatus(this OrderStatus orderStatus)
    {
        return orderStatus switch
        {
            OrderStatus.Pending => PaymentStatus.Pending,
            OrderStatus.Received => PaymentStatus.Completed,
            OrderStatus.Canceled => PaymentStatus.Cancelled,
            OrderStatus.InProgress or OrderStatus.Done or OrderStatus.Finished => PaymentStatus.Completed,
            _ => PaymentStatus.None
        };
    }
}