using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace NCommons.Logging
{
    /// <summary>
    /// Provides a default <see cref="ILoggingService"/>  implementation.
    /// </summary>
    public class LoggingService : ILoggingService
    {
        readonly IList<ILogger> _loggers;

        public LoggingService(ILogger defaultLogger)
        {
            _loggers = new List<ILogger>();
            _loggers.Add(defaultLogger);
        }

        public void Write(LogEntry log)
        {
            _loggers.ToList().ForEach(logger => logger.Write(log));
        }

        public void AddLogger(ILogger logger)
        {
            Contract.Assert(!_loggers.Contains(logger));
            _loggers.Add(logger);
        }

        public void RemoveLogger(ILogger logger)
        {
            _loggers.Remove(logger);
        }
    }
}