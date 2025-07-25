using Core.Entities;
using Core.Entities.Enums;
using Infrastructure.DataAccess.MongoAdapter.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Adapters.Gateways.MongoDbs.Entities;

[BsonIgnoreExtraElements]
[BsonDiscriminator("order")]
public class OrderMongoDb : MongoEntity
{
    [BsonRepresentation(BsonType.String)]
    internal OrderStatus Status { get; set; }

    [BsonRepresentation(BsonType.String)]
    internal PaymentMethod PaymentMethod { get; set; }

    public decimal TotalPrice { get; set; }
    public string? CustomerId { get; set; }
    public string? CustomerName { get; set; }
    public OrderPaymentMongoDb? Payment { get; set; }
    public IEnumerable<OrderItemMongoDb> Items { get; set; } = [];
    public string Notes { get; set; }

    public OrderMongoDb()
    {

    }

    public OrderMongoDb(Order order)
    {
        Status = order.Status;
        CustomerId = order.CustomerId;
        TotalPrice = order.TotalPrice;
        CustomerName = order.CustomerName;
        PaymentMethod = order.PaymentMethod;
        Items = order.Items.Select(item => new OrderItemMongoDb(item)).ToList();
        Payment = order.Payment is null ? null : new OrderPaymentMongoDb(order.Payment);
    }

    internal static Order ToCore(OrderMongoDb orderList)
    {
        return new Order()
        {
            Id = orderList.Id,
            Status = orderList.Status,
            TotalPrice = orderList.TotalPrice,
            CustomerId = orderList.CustomerId,
            CustomerName = orderList.CustomerName,
            Payment = orderList.Payment?.ToCore(),
            PaymentMethod = orderList.PaymentMethod,
            Items = OrderItemMongoDb.ToCore(orderList.Items),
        };
    }

    internal Order ToCore()
    {
        return new Order()
        {
            Id = Id,
            Status = Status,
            TotalPrice = TotalPrice,
            CustomerId = CustomerId,
            CustomerName = CustomerName,
            Payment = Payment?.ToCore(),
            PaymentMethod = PaymentMethod,
            Items = OrderItemMongoDb.ToCore(Items),
        };
    }

    internal static Pagination<Order> ToCore(PagedResult<OrderMongoDb> orderList)
    {
        return new Pagination<Order>()
        {
            Page = orderList.Page,
            Size = orderList.Size,
            TotalPages = orderList.TotalPages,
            TotalCount = orderList.TotalCount,
            Items = orderList.Items.Select(ToCore)
        };
    }
}