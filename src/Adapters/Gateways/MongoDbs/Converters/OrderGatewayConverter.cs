using Adapters.Gateways.MongoDbs.Entities;
using Adapters.Gateways.MongoDbs.Interfaces;
using Core.Entities;
using Core.Entities.Enums;
using Core.Gateways.Interfaces;

namespace Adapters.Gateways.MongoDbs.Converters;

public class OrderGatewayConverter : IOrderGateway
{
    private readonly IOrderMongoDbGateway _orderMongoDbGateway;

    public OrderGatewayConverter(IOrderMongoDbGateway orderMongoDbGateway)
    {
        _orderMongoDbGateway = orderMongoDbGateway;
    }

    public async Task<Pagination<Order>> GetAllByFilterAsync(OrderStatus? status, int size, int page, CancellationToken cancellationToken)
    {
        var orderList = await _orderMongoDbGateway.GetAllByFilterAsync(
            status,
            size,
            page,
            cancellationToken);

        return OrderMongoDb.ToCore(orderList);
    }

    public async Task<Order?> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        var orderMongoDb = await _orderMongoDbGateway.GetByIdAsync(id, cancellationToken);

        return orderMongoDb?.ToCore();
    }

    public async Task<Order> InsertOneAsync(Order order, CancellationToken cancellationToken)
    {
        var orderMongoDb = new OrderMongoDb(order);
        var insertedOrderMongoDb = await _orderMongoDbGateway.InsertOneAsync(orderMongoDb, cancellationToken);

        return insertedOrderMongoDb.ToCore();
    }

    public Task DeleteAsync(string id, CancellationToken cancellationToken)
    {
        return _orderMongoDbGateway.DeleteAsync(id, cancellationToken);
    }

    public Task UpdatePaymentAsync(string id, PaymentMethod paymentMethod, OrderPayment orderPayment, CancellationToken cancellationToken)
    {
        var orderPaymentMongoDb = new OrderPaymentMongoDb(orderPayment);
        return _orderMongoDbGateway.UpdatePaymentMethodAsync(id, paymentMethod, orderPaymentMongoDb, cancellationToken);
    }

    public async Task<Order> UpdateStatusAsync(string id, OrderStatus status, CancellationToken cancellationToken)
    {
        var orderMongoDb = await _orderMongoDbGateway.UpdateStatusAsync(id, status, cancellationToken);

        return orderMongoDb.ToCore();
    }
}