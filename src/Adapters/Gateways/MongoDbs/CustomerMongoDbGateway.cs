using Adapters.Gateways.MongoDbs.Entities;
using Adapters.Gateways.MongoDbs.Interfaces;
using Infrastructure.DataAccess.MongoAdapter;
using Infrastructure.DataAccess.MongoAdapter.Contexts.Interfaces;
using MongoDB.Driver;

namespace Adapters.Gateways.MongoDbs;

public class CustomerMongoDbGateway : MongoGatewayBase<CustomerMongoDb>, ICustomerMongoDbGateway
{
    public CustomerMongoDbGateway(IMongoContext mongoContext)
       : base(mongoContext)
    {

    }

    public async Task<CustomerMongoDb?> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        var filter = Builders<CustomerMongoDb>.Filter.Eq(e => e.Id, id);

        var options = new FindOptions();

        var customer = await _collection.Find(filter, options).FirstOrDefaultAsync(cancellationToken);

        return customer;
    }

    public async Task<CustomerMongoDb> InsertOneAsync(CustomerMongoDb customerMongoDb, CancellationToken cancellationToken)
    {
        var options = new InsertOneOptions();

        await _collection.InsertOneAsync(customerMongoDb, options, cancellationToken);

        return customerMongoDb;
    }
}