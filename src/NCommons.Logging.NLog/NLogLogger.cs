using NLog;

namespace NCommons.Logging.NLog
{
    public class NLogLogger : ILogger
    {
        const string DefaultLogger = "Default";
        readonly ILogWriter _logWriter;

        public NLogLogger(ILogWriter logWriter)
        {
            Logger logger = LogManager.GetLogger(DefaultLogger);
            _logWriter = logWriter ?? new NLogWriterMap(logger);
        }

        public void Write(LogEntry log)
        {
            _logWriter.Write(log);
        }
    }
}