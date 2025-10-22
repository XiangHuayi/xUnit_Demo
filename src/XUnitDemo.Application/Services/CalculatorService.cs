using XUnitDemo.Application.Abstractions;

namespace XUnitDemo.Application.Services;

public class CalculatorService : ICalculatorService
{
    public int Add(int a, int b) => a + b;
    public int Subtract(int a, int b) => a - b;
    public int Multiply(int a, int b) => a * b;
    public double Divide(double a, double b)
    {
        if (Math.Abs(b) < double.Epsilon) throw new DivideByZeroException("Cannot divide by zero.");
        return a / b;
    }
}