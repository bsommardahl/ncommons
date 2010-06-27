namespace NCommons.Logging
{
    /// <summary>
    /// Provides a contract for implementations of <see cref="ILogger"/>
    /// utilizing the Strategy Pattern for the logging persistance.
    /// </summary>
    public interface ILogWriter
    {
        void Write(LogEntry logEntry);
    }
}