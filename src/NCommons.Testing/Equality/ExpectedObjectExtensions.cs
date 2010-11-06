namespace NCommons.Testing.Equality
{
    public static class ExpectedObjectExtensions
    {
        public static ExpectedObject ToExpectedObject(this object expected)
        {
            return new ExpectedObject(expected).Configure(AddDefaultStrategies);
        }

        public static ExpectedObject WithDefaultStrategies(this ExpectedObject expectedObject)
        {
            return expectedObject.Configure(AddDefaultStrategies);
        }

        public static ExpectedObject IgnoreTypes(this ExpectedObject expectedObject)
        {
            return expectedObject.Configure(ctx => ctx.IgnoreTypes());
        }

        static void AddDefaultStrategies(IConfigurationContext context)
        {
            context.AddStrategy<ComparableComparisonStrategy>();
            context.AddStrategy<PrimitiveComparisonStrategy>();
            context.AddStrategy<EqualsOverrideComparisonStrategy>();
            context.AddStrategy<EnumerableComparisonStrategy>();
            context.AddStrategy<ClassComparisonStrategy>();
            context.AddStrategy<DefaultComparisonStrategy>();
        }
    }
}