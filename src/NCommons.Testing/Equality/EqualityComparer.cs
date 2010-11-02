using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NCommons.Testing.Equality
{
    public class EqualityComparer
    {
        readonly ConfigurationContext _configurationContext;
        readonly Stack<string> _stack = new Stack<string>();

        public EqualityComparer(ConfigurationContext configurationContext)
        {
            _configurationContext = configurationContext;
        }

        public bool AreEqual(object expected, object actual)
        {
            return AreEqual(expected, actual, String.Empty);
        }

        internal bool AreEqual(object expected, object actual, string member)
        {
            try
            {
                bool areEqual = true;

                if (!string.IsNullOrEmpty(member))
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
                                    .Write(new EqualityResult(isEqual, GetMemberPath(), expected, actual));
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

        string GetMemberPath()
        {
            var sb = new StringBuilder();

            sb.Append(String.Join(".", _stack.Reverse().ToArray()));
            sb.Replace(".[", "[");
            return sb.ToString();
        }
    }
}