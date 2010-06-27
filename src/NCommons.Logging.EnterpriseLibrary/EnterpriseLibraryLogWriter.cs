using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace NCommons.Logging.EnterpriseLibrary
{
    public class EnterpriseLibraryLogWriter : ILogWriter
    {
        readonly LogWriter _logWriter;

        public EnterpriseLibraryLogWriter(LogWriter logWriter)
        {
            _logWriter = logWriter;
        }

        public void Write(LogEntry logEntry)
        {
            Microsoft.Practices.EnterpriseLibrary.Logging.LogEntry enterpriseLibraryLogEntry =
                new EnterpriseLibraryLogEntryConverter().Convert(logEntry);
            _logWriter.Write(enterpriseLibraryLogEntry);
        }
    }
}