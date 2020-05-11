namespace Calc.Core.Abstraction
{
    public interface ICalculate
    {
        T Add<T>(T a, T b);

        T Subtract<T>(T a, T b);

        T Multiply<T>(T a, T b);

        T Divide<T>(T a, T b);
    }
}
