using System;

namespace NCommons.Testing.Equality
{
    public interface IComparisonStrategy
    {
        bool CanCompare(Type type);
        bool AreEqual(object expected, object actual, EqualityComparer equalityComparer);
    }
}