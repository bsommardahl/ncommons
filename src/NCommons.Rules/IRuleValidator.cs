namespace NCommons.Rules
{
    public interface IRuleValidator
    {
        RuleValidationResult Validate(object message);
    }
}