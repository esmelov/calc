using Calc.Core.Abstraction;
using System;
using System.Collections.Concurrent;
using System.Linq.Expressions;

namespace Calc.Core
{
    public class Calculate : ICalculate
    {
        private sealed class OperationCacheKey : IEquatable<OperationCacheKey>
        {
            public OperationCacheKey(Type type, Operation operation)
            {
                Type = type;
                Operation = operation;
            }

            public Type Type { get; }

            public Operation Operation { get; }

            public bool Equals(OperationCacheKey other)
            {
                if (ReferenceEquals(null, other)) return false;
                if (ReferenceEquals(this, other)) return true;
                return Equals(Type, other.Type) && Operation == other.Operation;
            }

            public override bool Equals(object obj)
            {
                return ReferenceEquals(this, obj) || obj is OperationCacheKey other && Equals(other);
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(Type, Operation);
            }

            public static bool operator ==(OperationCacheKey left, OperationCacheKey right)
            {
                return Equals(left, right);
            }

            public static bool operator !=(OperationCacheKey left, OperationCacheKey right)
            {
                return !Equals(left, right);
            }
        }

        private static readonly ConcurrentDictionary<OperationCacheKey, Delegate>
            _cache = new ConcurrentDictionary<OperationCacheKey, Delegate>();
        
        public T Add<T>(T a, T b)
            => GetFunc<T>(Operation.Add)(a, b);

        public T Subtract<T>(T a, T b)
            => GetFunc<T>(Operation.Subtract)(a, b);

        public T Multiply<T>(T a, T b)
            => GetFunc<T>(Operation.Multiply)(a, b);

        public T Divide<T>(T a, T b)
            => GetFunc<T>(Operation.Divide)(a, b);

        private static Func<T, T, T> GetFunc<T>(Operation operation)
            => (Func<T, T, T>) _cache.GetOrAdd(
                new OperationCacheKey(typeof(T), operation), BuildFunc);

        private static Delegate BuildFunc(OperationCacheKey key)
        {
            var paramA = Expression.Parameter(key.Type, "a");
            var paramB = Expression.Parameter(key.Type, "b");
            var expr = key.Operation switch
            {
                Operation.Add => Expression.Add(paramA, paramB),
                Operation.Subtract => Expression.Subtract(paramA, paramB),
                Operation.Multiply => Expression.Multiply(paramA, paramB),
                Operation.Divide => Expression.Divide(paramA, paramB),
                _ => throw new NotImplementedException()
            };
            var lambda = Expression.Lambda(expr, paramA, paramB);
            return lambda.Compile();
        }
    }
}
