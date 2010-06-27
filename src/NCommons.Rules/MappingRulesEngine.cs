using NCommons.Rules.Mapping;

namespace NCommons.Rules
{
    public class MappingRulesEngine : RulesEngine
    {
        readonly IMessageMapper _messageMapper;

        public MappingRulesEngine(IRulesValidator rulesValidator, IMessageMapper messageMapper, IMissingCommandStrategy missingCommandStrategy)
            : base(rulesValidator, missingCommandStrategy)
        {
            _messageMapper = messageMapper;
        }

        
        protected override MessageValidationResult OnValidateMessage(object message)
        {
            var messageConverter = new MessageConverter(_messageMapper);
            ConversionResult conversionResult = messageConverter.Convert(message);
            MessageValidationResult messageValidationResult = base.OnValidateMessage(conversionResult.TargetMessage);
            RuleValidationResult ruleValidationResult = messageValidationResult.Result;
            messageValidationResult.Result = messageConverter.AssociateResults(ruleValidationResult, conversionResult);
            messageValidationResult.ValidatedMessage = conversionResult.TargetMessage;
            return messageValidationResult;
        }
    }
}