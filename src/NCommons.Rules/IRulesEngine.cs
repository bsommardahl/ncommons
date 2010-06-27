namespace NCommons.Rules
{
    public interface IRulesEngine
    {
        ProcessResult Process<T>(T message);
    }
}