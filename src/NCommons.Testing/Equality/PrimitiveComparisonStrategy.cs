using System;

namespace NCommons.Testing.Equality
{
    public class PrimitiveComparisonStrategy : IComparisonStrategy
    {
        public bool CanCompare(Type type)
        {
            return type.IsPrimitive;
        }

        public bool AreEqual(object expected, object actual, IComparisonContext comparisonContext)
        {
            return expected.Equals(actual);
        }
    }
}