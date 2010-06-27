namespace NCommons.Rules
{
    public interface IMissingCommandStrategy
    {
        void Execute(object message);
    }
}