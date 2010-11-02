using System;
using System.Collections.Generic;

namespace NCommons.Testing.Equality
{
    public class ConfigurationContext
    {
        readonly List<IComparisonStrategy> _strategies = new List<IComparisonStrategy>();
        IWriter _writer = NullWriter.Instance;

        public List<IComparisonStrategy> Strategies
        {
            get { return _strategies; }
        }

        public IWriter Writer
        {
            get { return _writer; }
            set { _writer = value; }
        }

        public void AddStrategy<T>() where T : IComparisonStrategy, new()
        {
            Strategies.Add(new T());
        }

        public void AddStrategy(IComparisonStrategy comparisonStrategy)
        {
            Strategies.Add(comparisonStrategy);
        }
    }
}