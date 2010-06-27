using System;
using System.Collections.Generic;
using System.Linq;

namespace NCommons.Rules.Mapping
{
    public abstract class AssociationConfiguration<TSource> : IAssociationConfiguration
    {
        IEnumerable<Association<TSource>> _associations;
        Type _destinationType;

        public virtual Type GetDestinationMessageType()
        {
            return _destinationType;
        }

        public virtual string GetSourcePropertyNameFor(string destinationPropertyName)
        {
            Association<TSource> association =
                _associations.Where(a => a.DestinationProperty.Name.Equals(destinationPropertyName)).FirstOrDefault();

            if (association != null)
            {
                return association.SourcePropertyName;
            }

            return destinationPropertyName;
        }

        protected void ConfigureAssociationsFor<TDestination>(
            Action<AssociationExpression<TSource, TDestination>> configuration)
        {
            var associationConfiguration = new PrivateValidationAssociation<TSource, TDestination>();
            configuration(associationConfiguration);
            _associations = associationConfiguration.GetAssociations();
            _destinationType = typeof(TDestination);
        }

        class PrivateValidationAssociation<T, TDestination> : AssociationExpression<T, TDestination>
        {
            public IEnumerable<Association<T>> GetAssociations()
            {
                return Associations;
            }
        }
    }

   
}