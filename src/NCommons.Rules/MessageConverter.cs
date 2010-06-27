using System;
using Microsoft.Practices.ServiceLocation;
using NCommons.Rules.Mapping;

namespace NCommons.Rules
{
    public class MessageConverter
    {
        readonly IMessageMapper _messageMapper;

        public MessageConverter(IMessageMapper messageMapper)
        {
            _messageMapper = messageMapper;
        }

        public ConversionResult Convert(object message)
        {
            object convertedMessage = message;
            IAssociationConfiguration associationConfiguration = null;

            if (_messageMapper != null)
            {
                Type associationConfigurationType =
                    typeof (AssociationConfiguration<>).MakeGenericType(message.GetType());
                associationConfiguration =
                    (IAssociationConfiguration) ServiceLocator.Current.GetInstance(associationConfigurationType);

                if (associationConfiguration != null)
                {
                    Type destinationMessageType = associationConfiguration.GetDestinationMessageType();
                    var mappedMessage = _messageMapper.Map(message, message.GetType(), destinationMessageType);
                    convertedMessage = mappedMessage ?? message;
                }
            }

            return new ConversionResult(convertedMessage, associationConfiguration);
        }

        public RuleValidationResult AssociateResults(RuleValidationResult ruleValidationResult, ConversionResult conversionResult)
        {
            RuleValidationResult result = ruleValidationResult;

            if (conversionResult.AssociationConfiguration != null && !ruleValidationResult.IsValid)
            {
                var mappedMessageValidationResult = new RuleValidationResult();

                // translate properties
                foreach (RuleValidationFailure failure in ruleValidationResult.Failures)
                {
                    string originalPropertyName =
                        conversionResult.AssociationConfiguration.GetSourcePropertyNameFor(failure.PropertyName);
                    mappedMessageValidationResult.AddValidationFailure(new RuleValidationFailure(failure.Message,
                                                                                             originalPropertyName));
                    result = mappedMessageValidationResult;
                }
            }

            return result;
        }
    }
}