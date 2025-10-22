namespace XUnitDemo.Application.Abstractions;

public interface ICalculatorService
{
    int Add(int a, int b);
    int Subtract(int a, int b);
    int Multiply(int a, int b);
    double Divide(double a, double b);
}