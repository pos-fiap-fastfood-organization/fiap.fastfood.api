using Core.Gateways.Interfaces;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Gateways.Loggers;

public class StockLoggerGateway : IStockGateway
{
    private readonly ILogger<StockLoggerGateway> _logger;

    public StockLoggerGateway(ILogger<StockLoggerGateway> logger)
    {
        _logger = logger;
    }

    public void SendAuditLog(string auditLog)
    {
        _logger.LogInformation("{AuditLog}", auditLog);
    }
}