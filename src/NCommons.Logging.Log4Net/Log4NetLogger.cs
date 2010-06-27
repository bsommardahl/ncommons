using log4net;

namespace NCommons.Logging.Log4Net
{
    public class Log4NetLogger : ILogger
    {
        const string DefaultLogger = "Default";
        readonly ILogWriter _logWriter;
        
        public Log4NetLogger(ILogWriter logWriter)
        {
            var log = LogManager.GetLogger(DefaultLogger);
            _logWriter = logWriter ?? new Log4NetLogWriterMap(log);
        }

        public void Write(LogEntry log)
        {
            _logWriter.Write(log);
        }
    }
}