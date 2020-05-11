using Calc.Core.MathExpressions.Abstract;
using System;

namespace Calc.Core.MathExpressions.Extensions
{
    public static class MathExpressionsExtension
    {
        public static MathExpression<T> Add<T>(this MathExpression<T> left, MathExpression<T> right)
        {
            if (left == null || right == null)
                throw new ArgumentNullException($"{nameof(left)} or {nameof(right)} cannot be null");

            return new BinaryMathExpression<T>(left, right, Operation.Add);
        }

        public static MathExpression<T> Subtract<T>(this MathExpression<T> left, MathExpression<T> right)
        {
            if (left == null || right == null)
                throw new ArgumentNullException($"{nameof(left)} or {nameof(right)} cannot be null");

            return new BinaryMathExpression<T>(left, right, Operation.Subtract);
        }

        public static MathExpression<T> Multiply<T>(this MathExpression<T> left, MathExpression<T> right)
        {
            if (left == null || right == null)
                throw new ArgumentNullException($"{nameof(left)} or {nameof(right)} cannot be null");

            return new BinaryMathExpression<T>(left, right, Operation.Multiply);
        }

        public static MathExpression<T> Divide<T>(this MathExpression<T> left, MathExpression<T> right)
        {
            if (left == null || right == null)
                throw new ArgumentNullException($"{nameof(left)} or {nameof(right)} cannot be null");

            return new BinaryMathExpression<T>(left, right, Operation.Divide);
        }
    }
}
