namespace NCommons.Logging
{
    /// <summary>
    /// Provides the contract for loggers used by <see cref="ILoggingService"/>.
    /// </summary>
    public interface ILogger
    {
        void Write(LogEntry log);
    }
}