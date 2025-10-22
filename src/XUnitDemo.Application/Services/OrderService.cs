using Microsoft.Extensions.Logging;
using XUnitDemo.Application.Abstractions;
using XUnitDemo.Domain.Entities;

namespace XUnitDemo.Application.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _repository;
    private readonly ILogger<OrderService>? _logger;

    public OrderService(IOrderRepository repository, ILogger<OrderService>? logger = null)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _logger = logger;
    }

    public async Task<decimal> ProcessOrderAsync(Order order, CancellationToken cancellationToken = default)
    {
        if (order is null) throw new ArgumentNullException(nameof(order));
        if (order.Items.Count == 0) throw new InvalidOperationException("Order must contain items.");

        var total = order.Total;
        _logger?.LogInformation("Processing order {OrderId} with total {Total}", order.Id, total);
        await _repository.SaveAsync(order, cancellationToken);
        return total;
    }
}