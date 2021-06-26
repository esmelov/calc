using Calc.Core.MathExpressions.Abstract;
using System.Linq.Expressions;

namespace Calc.Core.MathExpressions
{
    public class UnaryMathExpression<T> : MathExpression<T>
        where T : struct
    {
        public UnaryMathExpression(T value)
            => Value = value;

        public T Value { get; }

        public override Expression ToExpression()
            => Expression.Constant(Value, Type);

        public static BinaryMathExpression<T> operator +(UnaryMathExpression<T> left, BinaryMathExpression<T> right)
            => new BinaryMathExpression<T>(left, right, Operation.Add);

        public static BinaryMathExpression<T> operator -(UnaryMathExpression<T> left, UnaryMathExpression<T> right)
            => new BinaryMathExpression<T>(left, right, Operation.Subtract);

        public static BinaryMathExpression<T> operator *(UnaryMathExpression<T> left, UnaryMathExpression<T> right)
            => new BinaryMathExpression<T>(left, right, Operation.Multiply);

        public static BinaryMathExpression<T> operator /(UnaryMathExpression<T> left, UnaryMathExpression<T> right)
            => new BinaryMathExpression<T>(left, right, Operation.Divide);
    }
}
