namespace XUnitDemo.Domain.Entities;

public class Order
{
    public Guid Id { get; }
    public DateTime CreatedAt { get; }
    public IReadOnlyList<OrderItem> Items { get; }

    public Order(Guid id, IEnumerable<OrderItem> items)
    {
        if (id == Guid.Empty) throw new ArgumentException("Order id cannot be empty", nameof(id));
        if (items is null) throw new ArgumentNullException(nameof(items));
        var list = items.ToList();
        if (list.Count == 0) throw new ArgumentException("Order must contain at least one item", nameof(items));
        Id = id;
        Items = list.AsReadOnly();
        CreatedAt = DateTime.UtcNow;
    }

    public decimal Total => Items.Sum(i => i.Total);
}