using System;
using System.Linq.Expressions;

namespace NCommons.Persistence
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Predicate { get; }
    }
}