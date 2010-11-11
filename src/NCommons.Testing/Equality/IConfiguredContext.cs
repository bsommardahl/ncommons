using System.Collections.Generic;
using System.Reflection;

namespace NCommons.Testing.Equality
{
    public interface IConfiguredContext
    {
        List<IComparisonStrategy> Strategies { get; }
        IWriter Writer { get; }
        bool IgnoreTypes { get; }
        BindingFlags GetFieldBindingFlags();
    }
}