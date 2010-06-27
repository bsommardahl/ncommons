using System;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Practices.ServiceLocation;
using NCommons.Rules;

namespace RulesEngineExample.Infrastructure
{
    public class FluentValidationRulesValidator : IRulesValidator
    {
        public RuleValidationResult Validate(object message)
        {
            var result = new RuleValidationResult();
            Type validatorType = typeof (AbstractValidator<>).MakeGenericType(message.GetType());
            var validator = (IValidator) ServiceLocator.Current.GetInstance(validatorType);
            ValidationResult validationResult = validator.Validate(message);

            if (!validationResult.IsValid)
            {
                foreach (ValidationFailure error in validationResult.Errors)
                {
                    var failure = new RuleValidationFailure(error.ErrorMessage, error.PropertyName);
                    result.AddValidationFailure(failure);
                }
            }

            return result;
        }
    }
}