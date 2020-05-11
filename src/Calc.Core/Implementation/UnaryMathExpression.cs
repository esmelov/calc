using Calc.Core.Abstraction;
using System.Linq.Expressions;

namespace Calc.Core
{
    public class UnaryMathExpression<T> : MathExpression<T>
    {
        public UnaryMathExpression(T value)
            => Value = value;

        public T Value { get; }

        public override Expression ToExpression()
            => Expression.Constant(Value, Type);
    }
}
