using System;
using System.Reflection;

namespace NCommons.Testing.Equality
{
    public class ClassComparisonStrategy : IComparisonStrategy
    {
        public bool CanCompare(Type type)
        {
            return (type.IsClass && !type.IsArray);
        }

        public bool AreEqual(object expected, object actual, IComparisonContext comparisonContext)
        {
            const bool equal = true;
            bool areEqual = comparisonContext.CompareProperties(expected, actual,
                                                                (pi, actualPropertyInfo) =>
                                                                CompareProperty(pi, actualPropertyInfo, expected, actual,
                                                                                comparisonContext, equal));

            return areEqual;
        }

        static bool CompareProperty(PropertyInfo pi, PropertyInfo actualPropertyInfo, object expected, object actual,
                             IComparisonContext comparisonContext, bool areEqual)
        {
            ParameterInfo[] indexes = pi.GetIndexParameters();

            if (indexes.Length == 0)
            {
                areEqual = CompareStandardProperty(pi, actualPropertyInfo, expected, actual, comparisonContext) &&
                           areEqual;
            }
            else
            {
                areEqual = CompareIndexedProperty(pi, expected, actual, indexes, comparisonContext) && areEqual;
            }

            return areEqual;
        }

        static bool CompareIndexedProperty(PropertyInfo pi, object expected, object actual, ParameterInfo[] indexes,
                                           IComparisonContext comparisonContext)
        {
            bool areEqual = true;

            foreach (ParameterInfo index in indexes)
            {
                if (index.ParameterType == typeof (int))
                {
                    PropertyInfo expectedCountPropertyInfo = expected.GetType().GetProperty("Count");

                    PropertyInfo actualCountPropertyInfo = actual.GetType().GetProperty("Count");

                    if (expectedCountPropertyInfo != null)
                    {
                        var expectedCount = (int) expectedCountPropertyInfo.GetValue(expected, null);
                        var actualCount = (int) actualCountPropertyInfo.GetValue(actual, null);

                        if (expectedCount != actualCount)
                        {
                            areEqual = false;
                            break;
                        }

                        for (int i = 0; i < expectedCount; i++)
                        {
                            object[] indexValues = {i};
                            object value1 = pi.GetValue(expected, indexValues);
                            object value2 = pi.GetValue(actual, indexValues);

                            if (!comparisonContext.AreEqual(value1, value2, pi.Name + "[" + i + "]"))
                            {
                                areEqual = false;
                            }
                        }
                    }
                }
            }

            return areEqual;
        }

        static bool CompareStandardProperty(PropertyInfo pi1, PropertyInfo pi2, object expected, object actual,
                                            IComparisonContext comparisonContext)
        {
            object value1 = pi1.GetValue(expected, null);

            if (pi2 == null)
            {
                return comparisonContext
                    .AreEqual(value1, Activator.CreateInstance(typeof (MissingMember<>)
                                                                   .MakeGenericType(pi1.PropertyType)), pi1.Name);
            }

            object value2 = pi2.GetValue(actual, null);
            return comparisonContext.AreEqual(value1, value2, pi1.Name);
        }
    }
}