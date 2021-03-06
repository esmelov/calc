﻿using Calc.Core.Abstraction;
using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Calc.Core
{
    public class Calculate : ICalculate
    {
        [DebuggerDisplay("Type: {Type}; Operation: {Operation}")]
        private readonly struct OperationCacheKey : IEquatable<OperationCacheKey>
        {
            public OperationCacheKey(Type type, Operation operation)
            {
                Type = type;
                Operation = operation;
            }

            public Type Type { get; }

            public Operation Operation { get; }

            public bool Equals(OperationCacheKey other)
                => other.Type == Type && other.Operation == Operation;

            public override bool Equals(object obj)
                => Equals((OperationCacheKey)obj);

            public override int GetHashCode()
                => HashCode.Combine(Type, Operation);

            public static bool operator ==(OperationCacheKey left, OperationCacheKey right)
                => Equals(left, right);

            public static bool operator !=(OperationCacheKey left, OperationCacheKey right)
                => !Equals(left, right);
        }

        private static readonly ConcurrentDictionary<OperationCacheKey, Delegate> _cache = new();

        public T Add<T>(T a, T b)
            => GetFunc<T>(Operation.Add)(a, b);

        public T Subtract<T>(T a, T b)
            => GetFunc<T>(Operation.Subtract)(a, b);

        public T Multiply<T>(T a, T b)
            => GetFunc<T>(Operation.Multiply)(a, b);

        public T Divide<T>(T a, T b)
            => GetFunc<T>(Operation.Divide)(a, b);

        private static Func<T, T, T> GetFunc<T>(Operation operation)
            => (Func<T, T, T>) _cache.GetOrAdd(new OperationCacheKey(typeof(T), operation), BuildFunc);

        private static Delegate BuildFunc(OperationCacheKey key)
        {
            var paramA = Expression.Parameter(key.Type, "a");
            var paramB = Expression.Parameter(key.Type, "b");
            var expr = key.Operation switch
            {
                Operation.Add => Expression.AddChecked(paramA, paramB),
                Operation.Subtract => Expression.SubtractChecked(paramA, paramB),
                Operation.Multiply => Expression.MultiplyChecked(paramA, paramB),
                Operation.Divide => Expression.Divide(paramA, paramB),
                _ => throw new NotSupportedException($"Not supported for {key.Operation}.")
            };
            var lambda = Expression.Lambda(expr, paramA, paramB);

            return lambda.Compile();
        }
    }
}
