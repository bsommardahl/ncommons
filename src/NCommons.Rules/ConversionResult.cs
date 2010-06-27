using NCommons.Rules.Mapping;

namespace NCommons.Rules
{
    public class ConversionResult
    {
        public ConversionResult(object targetMessage, IAssociationConfiguration associationConfiguration)
        {
            TargetMessage = targetMessage;
            AssociationConfiguration = associationConfiguration;
        }

        public object TargetMessage { get; private set; }
        public IAssociationConfiguration AssociationConfiguration { get; private set; }
    }
}