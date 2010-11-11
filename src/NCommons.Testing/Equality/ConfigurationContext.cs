using System;
using System.Collections.Generic;
using System.Reflection;

namespace NCommons.Testing.Equality
{
    public class ConfigurationContext : IConfigurationContext, IConfiguredContext
    {
        readonly List<IComparisonStrategy> _strategies = new List<IComparisonStrategy>();
        bool _ignoreTypes;
        IWriter _writer = NullWriter.Instance;
        MemberType _memberType;

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

        public void Include(MemberType memberType)
        {
             _memberType |= memberType;
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

        public BindingFlags GetFieldBindingFlags()
        {
            BindingFlags flags = BindingFlags.Default;

           if((_memberType & MemberType.PublicFields) == MemberType.PublicFields)
           {
               flags |= BindingFlags.Public | BindingFlags.Instance;
           }

            return flags;
        }
    }
}