using System;
using System.Linq.Expressions;
using System.Reflection;

namespace NCommons.Rules.Mapping
{
    /// <summary>
    /// Represents an association between the properties of a source and destination message
    /// used in relating instances of <see cref="RuleValidationFailure"/> back to the original message.
    /// </summary>
    /// <typeparam name="TSource">the source message type</typeparam>
    public class Association<TSource>
    {
        string _sourcePropertyName;

        public Association(PropertyInfo destinationProperty)
        {
            DestinationProperty = destinationProperty;
        }

        public PropertyInfo SourceProperty { get; private set; }
        public PropertyInfo DestinationProperty { get; private set; }

        public string SourcePropertyName
        {
            get { return (!string.IsNullOrWhiteSpace(_sourcePropertyName) ? _sourcePropertyName : SourceProperty.Name); }
        }

        public void Use(Expression<Func<TSource, object>> sourceProperty)
        {
            Expression expression = sourceProperty.Body;
            SourceProperty = PropertyInfoHelper.GetPropertyInfo(expression);
        }

        public void Use(string propertyName)
        {
            _sourcePropertyName = propertyName;
        }
    }
}