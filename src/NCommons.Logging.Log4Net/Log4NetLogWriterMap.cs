using System;
using System.Collections.Generic;
using System.Diagnostics;
using log4net;

namespace NCommons.Logging.Log4Net
{
    /// <summary>
    /// Default implementation of <see cref="LogWriterMap{T}"/> for Log4Net.
    /// </summary>
    public sealed class Log4NetLogWriterMap : LogWriterMap<TraceEventType>
    {
        const string Exception = "Exception";

        public Log4NetLogWriterMap(ILog logger)
        {
            var converter = new StringLogEntryConverter();

            Map = new Dictionary<TraceEventType, LogWriterProvider>
                      {
                          { TraceEventType.Warning, e => logger.Warn(converter.Convert(e), (Exception) e.ExtendedProperties.ValueOrDefault(Exception)) },
                          { TraceEventType.Verbose, e => logger.Debug(converter.Convert(e), (Exception) e.ExtendedProperties.ValueOrDefault(Exception)) },
                          { TraceEventType.Transfer, e => logger.Debug(converter.Convert(e), (Exception) e.ExtendedProperties.ValueOrDefault(Exception)) },
                          { TraceEventType.Suspend, e => logger.Debug(converter.Convert(e), (Exception) e.ExtendedProperties.ValueOrDefault(Exception)) },
                          { TraceEventType.Stop, e => logger.Debug(converter.Convert(e), (Exception) e.ExtendedProperties.ValueOrDefault(Exception)) },
                          { TraceEventType.Start, e => logger.Debug(converter.Convert(e), (Exception) e.ExtendedProperties.ValueOrDefault(Exception)) },
                          { TraceEventType.Resume, e => logger.Debug(converter.Convert(e), (Exception) e.ExtendedProperties.ValueOrDefault(Exception)) },
                          { TraceEventType.Information, e => logger.Info(converter.Convert(e), (Exception) e.ExtendedProperties.ValueOrDefault(Exception)) },
                          { TraceEventType.Error, e => logger.Error(converter.Convert(e), (Exception) e.ExtendedProperties.ValueOrDefault(Exception)) },
                          { TraceEventType.Critical, e => logger.Fatal(converter.Convert(e), (Exception) e.ExtendedProperties.ValueOrDefault(Exception)) }
                      };
        }

        protected override TraceEventType GetPropertyValue(LogEntry logEntry)
        {
            return logEntry.Severity;
        }
    }
}