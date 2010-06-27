using System;
using System.Linq;
using System.Linq.Expressions;

namespace NCommons.Persistence
{
    public class Specification<T> : ISpecification<T>
    {
        public Specification(Expression<Func<T, bool>> predicate)
        {
            Predicate = predicate;
        }

        public Expression<Func<T, bool>> Predicate { get; private set; }
    }


    // TODO: Revisit http://blogs.msdn.com/meek/archive/2008/05/02/linq-to-entities-combining-predicates.aspx
    // Will this work for Linq To Entities?
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