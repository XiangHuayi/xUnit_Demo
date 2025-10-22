namespace XUnitDemo.Domain.Entities;

public class Product
{
    public Guid Id { get; }
    public string Name { get; }
    public decimal Price { get; }

    public Product(Guid id, string name, decimal price)
    {
        if (id == Guid.Empty) throw new ArgumentException("Product id cannot be empty", nameof(id));
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Product name is required", nameof(name));
        if (price < 0) throw new ArgumentOutOfRangeException(nameof(price), "Price cannot be negative");
        Id = id;
        Name = name.Trim();
        Price = price;
    }
}