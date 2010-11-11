namespace NCommons.Testing.Equality
{
    public interface IConfigurationContext
    {
        void WithWriter(IWriter writer);
        void IgnoreTypes();
        void AddStrategy<T>() where T : IComparisonStrategy, new();
        void AddStrategy(IComparisonStrategy comparisonStrategy);
        void Include(MemberType memberType);
    }
}