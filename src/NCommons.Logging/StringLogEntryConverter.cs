namespace NCommons.Logging
{
    public class StringLogEntryConverter : ILogEntryConverter<string>
    {
        public virtual string Convert(LogEntry logEntry)
        {
            return logEntry.Message;
        }
    }
}