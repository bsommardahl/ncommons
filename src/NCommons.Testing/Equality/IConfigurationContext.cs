using System.Collections.Generic;

namespace NCommons.Testing.Equality
{
    public interface IConfigurationContext
    {
        void WithWriter(IWriter writer);
        void IgnoreTypes();
        void AddStrategy<T>() where T : IComparisonStrategy, new();
        void AddStrategy(IComparisonStrategy comparisonStrategy);
    }

    public interface IConfiguredContext
    {
        List<IComparisonStrategy> Strategies { get; }
        IWriter Writer { get; }
        bool IgnoreTypes { get; }
    }
}