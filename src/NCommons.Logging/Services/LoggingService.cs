using System;
using System.Collections.Generic;
using System.Linq;
using NCommons.Logging.Properties;

namespace NCommons.Logging
{
    /// <summary>
    /// Provides a default <see cref="ILoggingService"/>  implementation.
    /// </summary>
    public class LoggingService : ILoggingService
    {
        private readonly IList<ILogger> _loggers;

        public LoggingService(ILogger defaultLogger)
        {
            _loggers = new List<ILogger> {defaultLogger};
        }

        #region ILoggingService Members

        public void Write(LogEntry log)
        {
            _loggers.ToList().ForEach(logger => logger.Write(log));
        }

        public void AddLogger(ILogger logger)
        {
            if (_loggers.Contains(logger)) throw new ArgumentException(Resources.LoggerAlreadyExists, "logger");
            _loggers.Add(logger);
        }

        public void RemoveLogger(ILogger logger)
        {
            _loggers.Remove(logger);
        }

        #endregion
    }
}