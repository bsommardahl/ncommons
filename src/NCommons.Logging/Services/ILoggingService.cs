namespace NCommons.Logging
{
    /// <summary>
    /// Provides the contract for a generic logging service
    /// </summary>
    public interface ILoggingService
    {
        void Write(LogEntry log);
        void AddLogger(ILogger logger);
        void RemoveLogger(ILogger logger);
    }
}
