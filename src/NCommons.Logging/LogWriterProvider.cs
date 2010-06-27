namespace NCommons.Logging
{
    /// <summary>
    /// Delegate used by <see cref="LogWriterMap{T}"/>
    /// </summary>
    /// <param name="logEntry"></param>
    public delegate void LogWriterProvider(LogEntry logEntry);
}