namespace XUnitDemo.Domain.Entities;

public class OrderItem
{
    public Product Product { get; }
    public int Quantity { get; }

    public OrderItem(Product product, int quantity)
    {
        Product = product ?? throw new ArgumentNullException(nameof(product));
        if (quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be positive");
        Quantity = quantity;
    }

    public decimal Total => Product.Price * Quantity;
}