using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using XUnitDemo.Application.Abstractions;
using XUnitDemo.Application.Services;
using XUnitDemo.Domain.Entities;
using XUnitDemo.Infrastructure.Repositories;

var services = new ServiceCollection();
services.AddLogging(b => b.AddConsole());
services.AddSingleton<ICalculatorService, CalculatorService>();
services.AddSingleton<IOrderRepository, InMemoryOrderRepository>();
services.AddTransient<IOrderService, OrderService>();

var provider = services.BuildServiceProvider();

var calculator = provider.GetRequiredService<ICalculatorService>();
Console.WriteLine($"2 + 3 = {calculator.Add(2, 3)}");

var orderService = provider.GetRequiredService<IOrderService>();
var order = new Order(
    Guid.NewGuid(),
    new[]
    {
        new OrderItem(new Product(Guid.NewGuid(), "Keyboard", 50m), 2),
        new OrderItem(new Product(Guid.NewGuid(), "Mouse", 25m), 1)
    });
var total = await orderService.ProcessOrderAsync(order);
Console.WriteLine($"Processed order {order.Id} with total {total}");
