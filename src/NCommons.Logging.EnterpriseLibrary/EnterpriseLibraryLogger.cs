using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace NCommons.Logging.EnterpriseLibrary
{
    public class EnterpriseLibraryLogger : ILogger
    {
        readonly ILogWriter _logWriter;
        
        public EnterpriseLibraryLogger(IConfigurationSource configurationSource)
        {
            configurationSource = configurationSource ?? ConfigurationSourceFactory.Create();
            var logWriter = new LogWriterFactory(configurationSource).Create();
            _logWriter = new EnterpriseLibraryLogWriter(logWriter);
        }

        public EnterpriseLibraryLogger(ILogWriter logWriter)
        {
            _logWriter = logWriter;
        }

        public void Write(LogEntry logEntry)
        {
            _logWriter.Write(logEntry);
        }
    }
}