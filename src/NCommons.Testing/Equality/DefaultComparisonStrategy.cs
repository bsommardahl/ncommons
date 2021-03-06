﻿using System;

namespace NCommons.Testing.Equality
{
    public class DefaultComparisonStrategy : IComparisonStrategy
    {
        public bool CanCompare(Type type)
        {
            return true;
        }

        public bool AreEqual(object expected, object actual, EqualityComparer equalityComparer)
        {
            return expected.Equals(actual);
        }
    }
}