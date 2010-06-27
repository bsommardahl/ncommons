namespace NCommons.Logging.EnterpriseLibrary
{
    public class EnterpriseLibraryLogEntryConverter : ILogEntryConverter<Microsoft.Practices.EnterpriseLibrary.Logging.LogEntry>
    {
        public Microsoft.Practices.EnterpriseLibrary.Logging.LogEntry Convert(LogEntry logEntry)
        {
            var enterpriseLibraryLogEntry = new Microsoft.Practices.EnterpriseLibrary.Logging.LogEntry
                             {
                                 ActivityId = logEntry.ActivityId,
                                 AppDomainName = logEntry.AppDomainName,
                                 Categories = logEntry.Categories,
                                 EventId = logEntry.EventId,
                                 ExtendedProperties = logEntry.ExtendedProperties,
                                 MachineName = logEntry.MachineName,
                                 ManagedThreadName = logEntry.ManagedThreadName,
                                 Message = logEntry.Message,
                                 Priority = logEntry.Priority,
                                 ProcessId = logEntry.ProcessId,
                                 ProcessName = logEntry.ProcessName,
                                 RelatedActivityId = logEntry.RelatedActivityId,
                                 Severity = logEntry.Severity,
                                 TimeStamp = logEntry.TimeStamp,
                                 Title = logEntry.Title,
                                 Win32ThreadId = logEntry.Win32ThreadId
                             };

            return enterpriseLibraryLogEntry;
        }
    }
}