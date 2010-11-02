using System;

namespace NCommons.Testing.Equality
{
    public class ComparableComparisonStrategy : IComparisonStrategy
    {
        public bool CanCompare(Type type)
        {
            return (typeof (IComparable).IsAssignableFrom(type));
        }

        public bool AreEqual(object expected, object actual, EqualityComparer equalityComparer)
        {
            return (((IComparable) expected).CompareTo(actual) == 0);
        }
    }
}