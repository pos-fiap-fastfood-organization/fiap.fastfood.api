using Core.Entities;
using Core.Entities.Enums;
using Core.Gateways.Interfaces;
using Core.UseCases.Interfaces;
using System.Text;

namespace Core.UseCases;

public class StockUseCase : IStockUseCase
{
    private readonly IStockGateway _stockGateway;

    public StockUseCase(IStockGateway stockGateway)
    {
        _stockGateway = stockGateway;
    }

    public void RegisterOrder(Order order, CancellationToken cancellation, DateTime? finishDate = null)
    {
        if (order.Status is not OrderStatus.Finished)
        {
            return;
        }

        var auditLogBuilder = BuildAuditLog(order, ref finishDate);

        _stockGateway.SendAuditLog(auditLogBuilder.ToString());
    }

    private static StringBuilder BuildAuditLog(Order order, ref DateTime? finishDate)
    {
        finishDate = finishDate ?? DateTime.UtcNow;

        var itemsQuantity = order.Items
            .Select(item => new ItemQuantity
            {
                ItemId = item.Id!,
                Quantity = item.Amount
            });

        var auditLogBuilder = new StringBuilder();

        const string AUDIT_LOG_TEMPLATE = "The order {0} was finished in {1} with: {2}";

        foreach (var itemQuantity in itemsQuantity)
        {
            var auditLog = string.Format(
                AUDIT_LOG_TEMPLATE,
                order.Id,
                finishDate.Value.ToString("yyyy-MM-dd HH:mm:ss"),
                itemQuantity.ToString());

            auditLogBuilder.AppendLine(auditLog);
        }

        return auditLogBuilder;
    }
}