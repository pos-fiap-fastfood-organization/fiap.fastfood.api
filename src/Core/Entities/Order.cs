using Core.Entities.Enums;
using Core.Entities.Exceptions;

namespace Core.Entities;

public class Order
{
    private string? _id;
    private string? _customerId;
    private OrderStatus _status;
    private decimal _totalPrice;
    private string? _customerName;
    private string? _notes;
    private OrderPayment? _payment;
    private PaymentMethod _paymentMethod;
    private IEnumerable<OrderItem> _items = Enumerable.Empty<OrderItem>();

    public Order()
    {

    }

    public Order(
        string? id,
        string? customerId,
        string? customerName,
        IEnumerable<OrderItem> items,
        OrderStatus status,
        PaymentMethod paymentMethod,
        decimal totalPrice)
    {
        Id = id;
        CustomerId = customerId;
        CustomerName = customerName;
        Items = items;
        Status = status;
        PaymentMethod = paymentMethod;
        TotalPrice = totalPrice;
    }

    public Order(
        string? customerId,
        string? customerName,
        IEnumerable<OrderItem> items)
    {
        CustomerId = customerId;
        CustomerName = customerName;
        Items = items;
        TotalPrice = SumItems(items);
        Status = OrderStatus.Pending;
        PaymentMethod = PaymentMethod.None;
    }


    public string? Id { get => _id; set => _id = value; }
    public OrderPayment? Payment { get => _payment; set => _payment = value; }
    public string? CustomerId { get => _customerId; set => _customerId = value; }
    public IEnumerable<OrderItem> Items { get => _items; set => _items = value; }
    public string? CustomerName { get => _customerName; set => _customerName = value; }
    public OrderStatus Status { get => _status; set => _status = ValidateCategory(value); }
    public PaymentMethod PaymentMethod { get => _paymentMethod; set => _paymentMethod = value; }
    public decimal TotalPrice
    {
        get => _totalPrice;
        set => _totalPrice = OrderPropertyException.ThrowIfZeroOrNegative(value, nameof(TotalPrice));
    }
    public string? Notes { get => _notes; set => _notes = value; }

    private static OrderStatus ValidateCategory(OrderStatus value)
    {
        var isInvalidCategory = !Enum.IsDefined(typeof(OrderStatus), value) || value == OrderStatus.None;

        if (isInvalidCategory)
        {
            throw new OrderItemPropertyException(nameof(OrderStatus));
        }

        return value;
    }

    private static decimal SumItems(IEnumerable<OrderItem> items)
    {
        decimal total = 0;

        foreach (var item in items)
        {
            total += item.GetTotalPrice();
        }

        return total;
    }

    public void SetPayment(OrderPayment? orderPayment)
    {
        if (orderPayment is null)
        {
            throw new OrderItemPropertyException(nameof(orderPayment));
        }

        Payment = orderPayment;
    }

    internal void ConfirmPayment()
    {
        Status = OrderStatus.Received;
    }

    internal void Cancel(string reason)
    {
        Status = OrderStatus.Canceled;
        Notes = reason;
    }
}