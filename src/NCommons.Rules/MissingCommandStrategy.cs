namespace NCommons.Rules
{
    /// <summary>
    /// Default strategy for missing commands is to do nothing.
    /// </summary>
    public class MissingCommandStrategy : IMissingCommandStrategy
    {
        public void Execute(object message)
        {
            // Do nothing
        }
    }
}