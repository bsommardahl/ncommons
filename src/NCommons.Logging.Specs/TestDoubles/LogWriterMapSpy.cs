using System.Collections.Generic;
using System.Diagnostics;

namespace NCommons.Logging.Specs
{
    class LogWriterMapSpy : LogWriterMap<TraceEventType>
    {
        public bool WasDelegateCalled { get; set; }

        public LogWriterMapSpy()
        {
            Map = new Dictionary<TraceEventType, LogWriterProvider>
                      {
                          {TraceEventType.Resume, x => WasDelegateCalled = true}
                      };
        }

        protected override TraceEventType GetPropertyValue(LogEntry logEntry)
        {
            return logEntry.Severity;
        }
    }
}