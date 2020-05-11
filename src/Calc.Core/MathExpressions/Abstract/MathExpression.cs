using System;
using System.Linq.Expressions;

namespace Calc.Core.MathExpressions.Abstract
{
    public abstract class MathExpression<T>
        where T : struct
    {
        public Type Type => typeof(T);

        public abstract Expression ToExpression();

        public virtual Func<T> Compile()
            => Expression.Lambda<Func<T>>(ToExpression()).Compile();
    }
}
