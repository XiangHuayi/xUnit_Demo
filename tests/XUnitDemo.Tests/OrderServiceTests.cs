using Microsoft.Extensions.DependencyInjection;
using Moq;
using XUnitDemo.Application.Abstractions;
using XUnitDemo.Application.Services;
using XUnitDemo.Domain.Entities;
using Xunit;

namespace XUnitDemo.Tests;

public class OrderServiceTests
{
    [Fact]
    public async Task ProcessOrderAsync_ValidOrder_SavesAndReturnsTotal()
    {
        // Arrange
        var repoMock = new Mock<IOrderRepository>();
        var sut = new OrderService(repoMock.Object);
        var order = new Order(Guid.NewGuid(), new[]
        {
            new OrderItem(new Product(Guid.NewGuid(), "ItemA", 10m), 2), // 20
            new OrderItem(new Product(Guid.NewGuid(), "ItemB", 5m), 1)   // 5
        });

        // Act
        var total = await sut.ProcessOrderAsync(order);

        // Assert
        repoMock.Verify(r => r.SaveAsync(order, It.IsAny<CancellationToken>()), Times.Once);
        Assert.Equal(25m, total);
    }

    [Fact]
    public async Task ProcessOrderAsync_NullOrder_Throws()
    {
        var repoMock = new Mock<IOrderRepository>();
        var sut = new OrderService(repoMock.Object);
        await Assert.ThrowsAsync<ArgumentNullException>(() => sut.ProcessOrderAsync(null!));
    }

    [Fact]
    public void DependencyInjection_ResolvesOrderServiceAndRepository()
    {
        var services = new ServiceCollection();
        services.AddSingleton<IOrderRepository, XUnitDemo.Infrastructure.Repositories.InMemoryOrderRepository>();
        services.AddTransient<IOrderService, OrderService>();

        var provider = services.BuildServiceProvider();

        var orderService = provider.GetRequiredService<IOrderService>();
        var repo = provider.GetRequiredService<IOrderRepository>();

        Assert.NotNull(orderService);
        Assert.NotNull(repo);
    }

    [Fact]
    public void OrderItem_NonPositiveQuantity_Throws()
    {
        var product = new Product(Guid.NewGuid(), "BadQty", 1m);
        Assert.Throws<ArgumentOutOfRangeException>(() => new OrderItem(product, 0));
        Assert.Throws<ArgumentOutOfRangeException>(() => new OrderItem(product, -1));
    }
}