using XUnitDemo.Domain.Entities;

namespace XUnitDemo.Application.Abstractions;

public interface IOrderRepository
{
    Task SaveAsync(Order order, CancellationToken cancellationToken = default);
    Task<Order?> GetAsync(Guid id, CancellationToken cancellationToken = default);
}