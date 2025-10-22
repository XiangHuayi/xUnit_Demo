using XUnitDemo.Application.Services;
using Xunit;

namespace XUnitDemo.Tests;

public class CalculatorServiceTests
{
    private readonly CalculatorService _sut = new();

    [Fact]
    public void Add_TwoPositiveNumbers_ReturnsSum()
    {
        var result = _sut.Add(2, 3);
        Assert.Equal(5, result);
    }

    [Theory]
    [InlineData(0, 0, 0)]
    [InlineData(-1, -1, -2)]
    [InlineData(10, -3, 7)]
    [InlineData(int.MaxValue - 1, 1, int.MaxValue)]
    public void Add_VariousCases_ReturnsExpected(int a, int b, int expected)
    {
        var result = _sut.Add(a, b);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Divide_ByZero_Throws()
    {
        Assert.Throws<DivideByZeroException>(() => _sut.Divide(10, 0));
    }

    [Fact]
    public void Multiply_NegativeAndPositive_ReturnsNegative()
    {
        var result = _sut.Multiply(-2, 5);
        Assert.Equal(-10, result);
    }
}