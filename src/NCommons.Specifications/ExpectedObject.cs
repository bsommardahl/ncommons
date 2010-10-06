using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace NCommons.Specifications
{
    public class ExpectedObject<T> : IExpectedObject
    {
        readonly IDictionary<PropertyInfo, IValueComparer> _comparisons = new Dictionary<PropertyInfo, IValueComparer>();
        readonly IDictionary<PropertyInfo, object> _members = new Dictionary<PropertyInfo, object>();
        readonly T _expectedValue;
       

        public SetPart<T, TResult> Set<TResult>(Expression<Func<T, TResult>> propertyExpression)
        {
            return new SetPart<T, TResult>(this, propertyExpression, TODO);
        }

        public ComparisonPart<TMember> UsingComparison<TMember>(Func<TMember, TMember, bool> comparison)
        {
            return new ComparisonPart<TMember>(this, comparison);
        }

        //public ExpectedObject(T expectedValue)
        //{
        //    _expectedValue = expectedValue;
        //}

        //public ComparisonPart<TResult> For<TResult>(Expression<Func<T, TResult>> propertyExpression)
        //{
        //    var propertyInfo = (PropertyInfo) ((MemberExpression) (propertyExpression.Body)).Member;
        //    return new ComparisonPart<TResult>(this, propertyInfo);
        //}

        public bool Equals(T value)
        {
            return Equals(value, new NullErrorLog<T>());
        }

        public bool Equals(object value, IErrorLog errorLog, string nestedValue)
        {
            bool result = true;

            PropertyInfo[] publicProperties =
                _expectedValue.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);

            //foreach (PropertyInfo property in publicProperties)
            //{
            //    object expectedValue = property.GetValue(_expectedValue, null);
            //    object actualValue = property.GetValue(value, null);

            //    if (_comparisons.ContainsKey(property))
            //    {
            //        if (!_comparisons[property].Compare(expectedValue, actualValue))
            //        {
            //            result = false;
            //            errorLog.Write(string.Format("For member \'{0}\', expected \'{1}\' but found \'{2}\'.\n",
            //                                        property.Name, expectedValue, actualValue));
            //        }

            //        continue;
            //    }

            //    if (!expectedValue.Equals(actualValue))
            //    {
            //        result = false;
            //        errorLog.Write(string.Format("For member \'{0}\', expected \'{1}\' but found \'{2}\'.\n",
            //                                  property.Name, expectedValue, actualValue));
            //    }
            //}

            foreach (var member in _members)
            {
                PropertyInfo memberInfo = member.Key;
                object expected = member.Value;
                object actual = memberInfo.GetValue(value, null);

                if (_expectedValue is IExpectedObject)
                {
                    MethodInfo methodInfo = _expectedValue.GetType().GetMethod("Equals",
                                                                               BindingFlags.Instance |
                                                                               BindingFlags.NonPublic |
                                                                               BindingFlags.Public,
                                                                               null,
                                                                               new[]
                                                                                   {
                                                                                       typeof (object),
                                                                                       typeof (IErrorLog),
                                                                                       typeof(string)
                                                                                   },
                                                                               null);

                    methodInfo.Invoke(_expectedValue, new[] {actual, errorLog, nestedValue + memberInfo.Name + "."});
                    continue;
                }

                if (!expected.Equals(actual))
                {
                    result = false;
                    errorLog.Write(string.Format("For member \'{0}{1}\', expected \'{2}\' but found \'{3}\'.\n",
                                                nestedValue,
                                                memberInfo.Name,
                                                expected,
                                                actual));
                }
            }

            return result;
        }

        //public class ComparisonPart<TMember>
        //{
        //    readonly ExpectedObject<T> _expectedObject;
        //    readonly PropertyInfo _propertyInfo;

        //    public ComparisonPart(ExpectedObject<T> expectedObject, PropertyInfo propertyInfo)
        //    {
        //        _expectedObject = expectedObject;
        //        _propertyInfo = propertyInfo;
        //    }

        //    public ExpectedObject<T> CompareWith(Func<TMember, TMember, bool> comparision)
        //    {
        //        _expectedObject._comparisons.Add(_propertyInfo, new ValueComparer<TMember>(comparision));
        //        return _expectedObject;
        //    }
        //}

        public class ComparisonPart<TMember>
        {
            readonly ExpectedObject<T> _expectedObject;
            readonly Func<TMember, TMember, bool> _comparison;

            public ComparisonPart(ExpectedObject<T> expectedObject, Func<TMember, TMember, bool> comparison)
            {
                _expectedObject = expectedObject;
                _comparison = comparison;
            }

            public SetPart<T, TResult> Set<TResult>(Expression<Func<T, TResult>> propertyExpression)
            {
                return new SetPart<T, TResult>(_expectedObject, propertyExpression, _comparison);
            }
        }

        public class SetPart<T, TResult>
        {
            readonly ExpectedObject<T> _owner;
            readonly TResult _comparison;

            readonly PropertyInfo _propertyInfo;

            public SetPart(ExpectedObject<T> owner, Expression<Func<T, TResult>> propertyExpression, TResult comparison)
            {
                _owner = owner;
                _comparison = comparison;
                var memberExpression = propertyExpression.Body as MemberExpression;
                if (memberExpression == null)
                    throw new ArgumentException(@"Expression must contain the only member access operation",
                                                "propertyExpression");
                _propertyInfo = typeof(T).GetProperty(memberExpression.Member.Name);
            }

            public ExpectedObject<T> To(ExpectedObject<T> expectedValue)
            {
                _owner._members.Add(_propertyInfo, expectedValue);
                return _owner;
            }

            public ExpectedObject<T> To(object expectedValue)
            {
                _owner._members.Add(_propertyInfo, expectedValue);
                return _owner;
            }
        }

        public interface IValueComparer
        {
            bool Compare(object x, object y);
        }

        public class ValueComparer<TMember> : IValueComparer
        {
            readonly Func<TMember, TMember, bool> _comparision;

            public ValueComparer(Func<TMember, TMember, bool> comparision)
            {
                _comparision = comparision;
            }

            public bool Compare(object x, object y)
            {
                return Compare((TMember) x, (TMember) y);
            }

            public bool Compare(TMember x, TMember y)
            {
                return _comparision(x, y);
            }
        }
    }
}