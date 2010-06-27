namespace NCommons.Rules
{
    public interface IRulesValidator
    {
        RuleValidationResult Validate(object message);
    }
}