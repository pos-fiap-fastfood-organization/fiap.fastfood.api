using Core.DTOs.Orders;
using Core.Entities.Enums;
using Infrastructure.DataAccess.MongoAdapter.Entities;
using Infrastructure.Gateways.MongoDbs.Entities;

namespace Infrastructure.Gateways.MongoDbs.Interfaces;

public interface IOrderMongoDbGateway
{
    Task<PagedResult<OrderMongoDb>> GetAllByFilterAsync(OrderFilter filter, CancellationToken cancellationToken);

    Task<OrderMongoDb> InsertOneAsync(OrderMongoDb order, CancellationToken cancellationToken);

    Task<OrderMongoDb?> GetByIdAsync(string id, CancellationToken cancellationToken);

    Task DeleteAsync(string id, CancellationToken cancellationToken);

    Task UpdatePaymentMethodAsync(
        string id,
        PaymentMethod paymentMethod,
        OrderPaymentMongoDb orderPaymentMongoDb,
        CancellationToken cancellationToken);
    Task<OrderMongoDb> UpdateStatusAsync(
        string id,
        OrderStatus status,
        CancellationToken cancellationToken);
}