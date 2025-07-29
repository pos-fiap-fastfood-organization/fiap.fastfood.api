using Adapters.Gateways.MongoDbs.Entities;
using Core.Entities.Enums;
using Infrastructure.DataAccess.MongoAdapter.Entities;

namespace Adapters.Gateways.MongoDbs.Interfaces;

public interface IOrderMongoDbGateway
{
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
        string? notes,
        CancellationToken cancellationToken);

    Task<PagedResult<OrderMongoDb>> GetAllByFilterAsync(
        OrderStatus? status,
        int size,
        int page,
        CancellationToken cancellationToken);

    Task<IEnumerable<OrderMongoDb>> GetAllPendingAsync(CancellationToken cancellationToken);
}