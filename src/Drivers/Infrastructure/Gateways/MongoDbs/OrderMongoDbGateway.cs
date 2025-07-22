using Core.DTOs.Orders;
using Core.Entities.Enums;
using Infrastructure.DataAccess.MongoAdapter;
using Infrastructure.DataAccess.MongoAdapter.Contexts.Interfaces;
using Infrastructure.DataAccess.MongoAdapter.Entities;
using Infrastructure.Gateways.MongoDbs.Entities;
using Infrastructure.Gateways.MongoDbs.Interfaces;
using MongoDB.Driver;

namespace Infrastructure.Gateways.MongoDbs;

public class OrderMongoDbGateway : MongoGatewayBase<OrderMongoDb>, IOrderMongoDbGateway
{
    public OrderMongoDbGateway(IMongoContext mongoContext) : base(mongoContext)
    {
    }

    public async Task<PagedResult<OrderMongoDb>> GetAllByFilterAsync(OrderFilter orderFilter, CancellationToken cancellationToken)
    {
        var builder = Builders<OrderMongoDb>.Filter;
        var finalFilter = Builders<OrderMongoDb>.Filter.Empty;
        var filters = new List<FilterDefinition<OrderMongoDb>>();

        if (orderFilter.Status is not null || orderFilter.Status is OrderStatus.None)
        {
            filters.Add(builder.Eq(order => order.Status, orderFilter.Status));
            ;

            finalFilter = builder.And(filters);
        }

        var pagedResult = await GetPagedAsync(orderFilter.Page, orderFilter.Size, finalFilter, cancellationToken);

        return pagedResult;
    }

    public async Task<OrderMongoDb> InsertOneAsync(OrderMongoDb order, CancellationToken cancellationToken)
    {
        await _collection.InsertOneAsync(order, null, cancellationToken);

        return order;
    }

    public async Task DeleteAsync(string id, CancellationToken cancellationToken)
    {
        var filter = Builders<OrderMongoDb>.Filter.Eq(entity => entity.Id, id);

        await _collection.DeleteOneAsync(filter, cancellationToken);
    }

    public async Task<OrderMongoDb?> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        var filter = Builders<OrderMongoDb>.Filter.Eq(entity => entity.Id, id);
        var order = await _collection.Find(filter).FirstOrDefaultAsync(cancellationToken);

        return order;
    }

    public Task UpdatePaymentMethodAsync(string id, PaymentMethod paymentMethod, OrderPaymentMongoDb orderPaymentMongoDb, CancellationToken cancellationToken)
    {
        var filter = Builders<OrderMongoDb>.Filter.Eq(entity => entity.Id, id);
        var update = Builders<OrderMongoDb>.Update
            .Set(entity => entity.PaymentMethod, paymentMethod)
            .Set(entity => entity.Payment, orderPaymentMongoDb)
            ;

        var options = new FindOneAndUpdateOptions<OrderMongoDb>
        {
            ReturnDocument = ReturnDocument.After
        };

        return _collection.FindOneAndUpdateAsync(filter, update, options, cancellationToken);
    }

    public Task<OrderMongoDb> UpdateStatusAsync(string id, OrderStatus status, CancellationToken cancellationToken)
    {
        var filter = Builders<OrderMongoDb>.Filter.Eq(entity => entity.Id, id);
        var update = Builders<OrderMongoDb>.Update.Set(entity => entity.Status, status);

        var options = new FindOneAndUpdateOptions<OrderMongoDb>
        {
            ReturnDocument = ReturnDocument.After
        };

        var order = _collection.FindOneAndUpdateAsync(filter, update, options, cancellationToken: cancellationToken);

        return order;
    }
}