namespace Core.Entities.Enums;

public static class OrderStatusExtensions
{
    public static PaymentStatus ToPaymentStatus(this OrderStatus orderStatus)
    {
        return orderStatus switch
        {
            OrderStatus.Pending => PaymentStatus.Pending,
            OrderStatus.Received => PaymentStatus.Approved,
            OrderStatus.Canceled => PaymentStatus.Refused,
            OrderStatus.InProgress or OrderStatus.Done or OrderStatus.Finished => PaymentStatus.Approved,
            _ => PaymentStatus.None
        };
    }
}