using System;
using System.Collections.Generic;

namespace NCommons.Rules
{
    public class RulesEngine : IRulesEngine
    {
        readonly IRuleValidator _ruleValidator;
        readonly IMissingCommandStrategy _missingCommandStrategy;
        
        public RulesEngine(IRuleValidator ruleValidator, IMissingCommandStrategy missingCommandStrategy)
        {
            _ruleValidator = ruleValidator;
            _missingCommandStrategy = missingCommandStrategy;
        }

        public ProcessResult Process<T>(T message)
        {
            // Validate message
            MessageValidationResult messageValidationResult = OnValidateMessage(message);
            RuleValidationResult ruleValidationResult = messageValidationResult.Result;

            // Process message
            IEnumerable<ReturnValue> returnValues = null;
            if (ruleValidationResult.IsValid)
                returnValues = ProcessMessage(messageValidationResult.ValidatedMessage);

            return GetProcessResult(ruleValidationResult, returnValues);
        }

        protected virtual MessageValidationResult OnValidateMessage(object message)
        {
            RuleValidationResult result = _ruleValidator.Validate(message);
            return new MessageValidationResult {Result = result, ValidatedMessage = message};
        }

        IEnumerable<ReturnValue> ProcessMessage(object message)
        {
            Type processorType = typeof (CommandProcessor<>).MakeGenericType(message.GetType());
            var processor = (ICommandProcessor) Activator.CreateInstance(processorType);
            return processor.Process(message, _missingCommandStrategy);
        }

        static ProcessResult GetProcessResult(RuleValidationResult ruleValidationResult,
                                              IEnumerable<ReturnValue> returnValues)
        {
            var processResult = new ProcessResult();

            if (!ruleValidationResult.IsValid)
                foreach (RuleValidationFailure failure in ruleValidationResult.Failures)
                    processResult.AddValidationFailure(failure);

            if (returnValues != null)
                foreach (ReturnValue returnValue in returnValues)
                    processResult.AddReturnItem(returnValue);

            return processResult;
        }
    }
}