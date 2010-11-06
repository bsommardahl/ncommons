using System.Collections.Generic;

namespace NCommons.Testing.Equality
{
    public class ConfigurationContext : IConfigurationContext, IConfiguredContext
    {
        readonly List<IComparisonStrategy> _strategies = new List<IComparisonStrategy>();
        bool _ignoreTypes;
        IWriter _writer = NullWriter.Instance;

        public void WithWriter(IWriter writer)
        {
            _writer = writer;
        }

        void IConfigurationContext.IgnoreTypes()
        {
            _ignoreTypes = true;
        }

        public void AddStrategy<T>() where T : IComparisonStrategy, new()
        {
            Strategies.Add(new T());
        }

        public void AddStrategy(IComparisonStrategy comparisonStrategy)
        {
            Strategies.Add(comparisonStrategy);
        }

        public List<IComparisonStrategy> Strategies
        {
            get { return _strategies; }
        }

        public IWriter Writer
        {
            get { return _writer; }
        }

        bool IConfiguredContext.IgnoreTypes
        {
            get { return _ignoreTypes; }
        }
    }
}