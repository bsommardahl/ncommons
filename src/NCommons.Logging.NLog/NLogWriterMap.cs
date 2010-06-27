using System.Collections.Generic;
using System.Diagnostics;
using NLog;

namespace NCommons.Logging.NLog
{
    public sealed class NLogWriterMap : LogWriterMap<TraceEventType>
    {
        public NLogWriterMap(Logger logger)
        {
            var converter = new StringLogEntryConverter();

            Map = new Dictionary<TraceEventType, LogWriterProvider>
                      {
                          {TraceEventType.Warning, e => logger.Warn(converter.Convert(e))},
                          {TraceEventType.Verbose, e => logger.Debug(converter.Convert(e))},
                          {TraceEventType.Transfer, e => logger.Debug(converter.Convert(e))},
                          {TraceEventType.Suspend, e => logger.Debug(converter.Convert(e))},
                          {TraceEventType.Stop, e => logger.Debug(converter.Convert(e))},
                          {TraceEventType.Start, e => logger.Debug(converter.Convert(e))},
                          {TraceEventType.Resume, e => logger.Debug(converter.Convert(e))},
                          {TraceEventType.Information, e => logger.Info(converter.Convert(e))},
                          {TraceEventType.Error, e => logger.Error(converter.Convert(e))},
                          {TraceEventType.Critical, e => logger.Fatal(converter.Convert(e))}
                      };
        }

        protected override TraceEventType GetPropertyValue(LogEntry logEntry)
        {
            return logEntry.Severity;
        }
    }
}