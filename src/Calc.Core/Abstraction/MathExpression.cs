using System;
using System.Linq.Expressions;

namespace Calc.Core.Abstraction
{
    public abstract class MathExpression<T>
    {
        public Type Type => typeof(T);

        public abstract Expression ToExpression();

        public virtual Func<T> Compile()
            => Expression.Lambda<Func<T>>(ToExpression()).Compile();
    }
}
