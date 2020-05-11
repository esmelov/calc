using Calc.Core.MathExpressions.Abstract;
using System;
using System.Linq.Expressions;

namespace Calc.Core.MathExpressions
{
    public class BinaryMathExpression<T> : MathExpression<T>
        where T : struct
    {
        public BinaryMathExpression(MathExpression<T> left, MathExpression<T> right, Operation operation)
        {
            Left = left ?? throw new ArgumentNullException(nameof(left));
            Right = right ?? throw new ArgumentNullException(nameof(right));
            Operation = operation;
        }

        public MathExpression<T> Left { get; }

        public MathExpression<T> Right { get; }

        public Operation Operation { get; }

        public override Expression ToExpression()
        {
            var leftExpr = Left.ToExpression();
            var rightExpr = Right.ToExpression();

            return Operation switch
            {
                Operation.Add => Expression.AddChecked(leftExpr, rightExpr),
                Operation.Subtract => Expression.SubtractChecked(leftExpr, rightExpr),
                Operation.Multiply => Expression.MultiplyChecked(leftExpr, rightExpr),
                Operation.Divide => Expression.Divide(leftExpr, rightExpr),
                _ => throw new NotSupportedException($"Not supported for {Operation}.")
            };
        }
    }
}
