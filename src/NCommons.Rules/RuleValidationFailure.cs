namespace NCommons.Rules
{
    public class RuleValidationFailure
    {
        public RuleValidationFailure(string message, string propertyName)
        {
            Message = message;
            PropertyName = propertyName;
        }

        public string Message { get; private set; }
        public string PropertyName { get; private set; }
    }
}