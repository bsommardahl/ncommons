using System;

namespace NCommons.Testing.Specs.TestTypes
{
    public class ComparableType : IComparable
    {
        readonly bool _isEqual;

        public ComparableType(bool isEqual)
        {
            _isEqual = isEqual;
        }

        public int CompareTo(object obj)
        {
            return _isEqual ? 0 : 1;
        }
    }
}