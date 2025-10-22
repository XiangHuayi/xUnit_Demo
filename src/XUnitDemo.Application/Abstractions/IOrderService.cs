using XUnitDemo.Domain.Entities;

namespace XUnitDemo.Application.Abstractions;

public interface IOrderService
{
    Task<decimal> ProcessOrderAsync(Order order, CancellationToken cancellationToken = default);
}