namespace NCommons.Logging
{
    /// <summary>
    /// Provides a contract for converting an instance of <see cref="LogEntry"/> to a type
    /// used by a specific implementation of <see cref="ILogger"/>.
    /// </summary>
    public interface ILogEntryConverter<T>
    {
        T Convert(LogEntry logEntry);
    }
}