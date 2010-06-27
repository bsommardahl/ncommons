namespace NCommons.Rules
{
    public interface ICommand<in TMessage>
    {
        ReturnValue Execute(TMessage message);
    }
}