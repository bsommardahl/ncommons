using System;
using System.Diagnostics;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging.TraceListeners;

namespace NCommons.Logging.EnterpriseLibrary.Specs
{
    [ConfigurationElementType(typeof(CustomTraceListenerData))]
    public class TraceListenerSpy : CustomTraceListener
    {
        public static string Message { get; set; }

        public override void Write(string message)
        {
            Message = message;
        }

        public override void WriteLine(string message)
        {
            var formatter = Formatter;
            Message = message;
        }

        /// <summary>
        /// Intercepts the tracing request to format the object to trace.
        /// </summary>
        /// <remarks>
        /// Formatting is only performed if the object to trace is a <see cref="LogEntry"/> and the formatter is set.
        /// </remarks>
        /// <param name="eventCache">The context information.</param>
        /// <param name="source">The trace source.</param>
        /// <param name="eventType">The severity.</param>
        /// <param name="id">The event id.</param>
        /// <param name="data">The object to trace.</param>
        public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
        {
            if ((this.Filter == null) || this.Filter.ShouldTrace(eventCache, source, eventType, id, null, null, data, null))
            {
                if (data is Microsoft.Practices.EnterpriseLibrary.Logging.LogEntry)
                {
                    if (this.Formatter != null)
                    {
                        Write(this.Formatter.Format(data as Microsoft.Practices.EnterpriseLibrary.Logging.LogEntry));
                    }
                    else
                    {
                        base.TraceData(eventCache, source, eventType, id, data);
                    }
                }
                else
                {
                    base.TraceData(eventCache, source, eventType, id, data);
                }
            }
        }

        public static void Reset()
        {
            Message = String.Empty;
        }
    }
}