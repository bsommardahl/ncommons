﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace NCommons.Testing.Equality
{
    public class EqualityComparer : IComparisonContext
    {
        readonly IConfiguredContext _configurationContext;
        readonly Stack<string> _stack = new Stack<string>();

        public EqualityComparer(IConfiguredContext configurationContext)
        {
            _configurationContext = configurationContext;
        }

        public bool AreEqual(object expected, object actual)
        {
            return AreEqual(expected, actual, (actual != null) ? actual.GetType().Name : string.Empty);
        }

        public bool AreEqual(object expected, object actual, string member)
        {
            try
            {
                bool areEqual = true;
                _stack.Push(member);

                if (ReferenceEquals(expected, actual))
                {
                    if (!string.IsNullOrEmpty(member))
                    {
                        _configurationContext.Writer.Write(new EqualityResult(true, GetMemberPath(), expected, actual));
                    }

                    return true;
                }

                if (expected == null || actual == null)
                {
                    _configurationContext.Writer.Write(new EqualityResult(false, GetMemberPath(), expected, actual));
                    return false;
                }

                if (_configurationContext.IgnoreTypes)
                {
                    if (actual.GetType() == typeof (MissingMember<>).MakeGenericType(expected.GetType()))
                    {
                        _configurationContext.Writer.Write(new EqualityResult(false, GetMemberPath(), expected, actual));
                        return false;
                    }
                }
                else if (!_configurationContext.IgnoreTypes && !expected.GetType().IsAssignableFrom(actual.GetType()))
                {
                    _configurationContext.Writer.Write(new EqualityResult(false, GetMemberPath(), expected, actual));
                    return false;
                }

                foreach (IComparisonStrategy strategy in _configurationContext.Strategies)
                {
                    if (strategy.CanCompare(expected.GetType()))
                    {
                        bool isEqual = strategy.AreEqual(expected, actual, this);

                        if (!isEqual)
                        {
                            if (_stack.Count > 0)
                            {
                                _configurationContext.Writer
                                    .Write(new EqualityResult(false, GetMemberPath(), expected, actual));
                            }
                            areEqual = false;
                        }

                        break;
                    }
                }

                return areEqual;
            }
            finally
            {
                if (_stack.Count > 0)
                    _stack.Pop();
            }
        }

        public bool CompareProperties(object expected, object actual, Func<PropertyInfo, PropertyInfo, bool> propertyComparison)
        {
            PropertyInfo[] expectedPropertyInfos = expected.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            PropertyInfo[] actualPropertyInfos = actual.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            bool areEqual = true;
            expectedPropertyInfos.ToList().ForEach(pi =>
                {

                    areEqual = propertyComparison(pi,
                                                  actualPropertyInfos.Where(p => p.Name.Equals(pi.Name)).SingleOrDefault
                                                      ()) & areEqual;
                });

            return areEqual;
        }

        string GetMemberPath()
        {
            var sb = new StringBuilder();

            sb.Append(String.Join(".", _stack.Reverse().ToArray()));
            sb.Replace(".[", "[");
            return sb.ToString();
        }
    }
}