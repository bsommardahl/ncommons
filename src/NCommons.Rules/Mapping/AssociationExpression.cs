using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace NCommons.Rules.Mapping
{
    /// <summary>
    /// Provides the fluent interface grammar for expressing an association between
    /// a source and destination message <see cref="PropertyInfo"/>.
    /// </summary>
    /// <typeparam name="TSource">the source message</typeparam>
    /// <typeparam name="TDestination">the destination message</typeparam>
    public class AssociationExpression<TSource, TDestination>
    {
        readonly IList<Association<TSource>> _associations = new List<Association<TSource>>();

        protected IList<Association<TSource>> Associations
        {
            get { return _associations; }
        }

        public Association<TSource> For(Expression<Func<TDestination, object>> destinationProperty)
        {
            Expression expression = destinationProperty.Body;
            PropertyInfo propertyInfo = PropertyInfoHelper.GetPropertyInfo(expression);
            var association = new Association<TSource>(propertyInfo);
            _associations.Add(association);
            return association;
        }
    }
}