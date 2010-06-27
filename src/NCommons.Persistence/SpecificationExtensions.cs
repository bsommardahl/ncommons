using System;
using System.Linq;
using System.Linq.Expressions;

namespace NCommons.Persistence
{
    public static class SpecificationExtensions
    {
        public static Specification<T> And<T>(this Specification<T> left, Specification<T> right)
        {
            InvocationExpression rightInvoke = Expression.Invoke(right.Predicate,
                                                                 left.Predicate.Parameters.Cast<Expression>());
            BinaryExpression newExpression = Expression.MakeBinary(ExpressionType.AndAlso, left.Predicate.Body,
                                                                   rightInvoke);
            return new Specification<T>(
                Expression.Lambda<Func<T, bool>>(newExpression, left.Predicate.Parameters)
                );
        }

        public static Specification<T> Or<T>(this Specification<T> left, Specification<T> right)
        {
            InvocationExpression rightInvoke = Expression.Invoke(right.Predicate,
                                                                 left.Predicate.Parameters.Cast<Expression>());
            BinaryExpression newExpression = Expression.MakeBinary(ExpressionType.Or, left.Predicate.Body,
                                                                   rightInvoke);
            return new Specification<T>(
                Expression.Lambda<Func<T, bool>>(newExpression, left.Predicate.Parameters)
                );
        }
    }
}