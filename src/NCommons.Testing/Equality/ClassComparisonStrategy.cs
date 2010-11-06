using System;
using System.Linq;
using System.Reflection;

namespace NCommons.Testing.Equality
{
    public class ClassComparisonStrategy : IComparisonStrategy
    {
        public bool CanCompare(Type type)
        {
            return (type.IsClass && !type.IsArray);
        }

        public bool AreEqual(object expected, object actual, EqualityComparer equalityComparer)
        {
            bool areEqual = true;

            PropertyInfo[] expectedPropertyInfos =
                expected.GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            PropertyInfo[] actualPropertyInfos =
                actual.GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (PropertyInfo pi in expectedPropertyInfos)
            {
                PropertyInfo actualPropertyInfo =
                    actualPropertyInfos.Where(p => p.Name.Equals(pi.Name)).SingleOrDefault();
                ParameterInfo[] indexes = pi.GetIndexParameters();
                
                if (indexes.Length == 0)
                {
                    areEqual = CompareStandardProperty(pi, actualPropertyInfo, expected, actual, equalityComparer) &&
                               areEqual;
                }
                else
                {
                    areEqual = CompareIndexedProperty(pi, expected, actual, indexes, equalityComparer) && areEqual;
                }
            }

            return areEqual;
        }

        bool CompareIndexedProperty(PropertyInfo pi, object expected, object actual, ParameterInfo[] indexes,
                                    EqualityComparer equalityComparer)
        {
            bool areEqual = true;

            foreach (ParameterInfo index in indexes)
            {
                if (index.ParameterType == typeof (int))
                {
                    int expectedCount = 0;
                    PropertyInfo expectedCountPropertyInfo = expected.GetType().GetProperty("Count");

                    int actualCount = 0;
                    PropertyInfo actualCountPropertyInfo = actual.GetType().GetProperty("Count");

                    if (expectedCountPropertyInfo != null)
                    {
                        expectedCount = (int) expectedCountPropertyInfo.GetValue(expected, null);
                        actualCount = (int) actualCountPropertyInfo.GetValue(actual, null);

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

                            if (!equalityComparer.AreEqual(value1, value2, pi.Name + "[" + i + "]"))
                            {
                                areEqual = false;
                            }
                        }
                    }
                }
            }

            return areEqual;
        }

        bool CompareStandardProperty(PropertyInfo pi1, PropertyInfo pi2, object expected, object actual,
                                     EqualityComparer equalityComparer)
        {
            object value1 = pi1.GetValue(expected, null);

            if (pi2 == null)
            {
                return equalityComparer
                    .AreEqual(value1, Activator.CreateInstance(typeof(MissingMember<>)
                    .MakeGenericType(pi1.PropertyType)), pi1.Name);
            }
            
            object value2 = pi2.GetValue(actual, null);
            return equalityComparer.AreEqual(value1, value2, pi1.Name);
        }
    }
}