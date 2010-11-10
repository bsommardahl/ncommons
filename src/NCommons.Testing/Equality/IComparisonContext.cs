using System;
using System.Reflection;

namespace NCommons.Testing.Equality
{
    public interface IComparisonContext
    {
        bool AreEqual(object expected, object actual, string member);
        bool CompareProperties(object expected, object actual, Func<PropertyInfo, PropertyInfo, bool> comparison);
    }
}