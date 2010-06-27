using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace NCommons.Logging
{
    /// <summary>
    /// Provides a suite of convenience methods for <see cref="ILoggingService"/>.
    /// </summary>
    public static class LoggingServiceExtensions
    {
        public static void Write(this ILoggingService loggingService, object message)
        {
            var logEntry = new LogEntry {Message = message.ToString()};
            loggingService.Write(logEntry);
        }

        public static void Write(this ILoggingService loggingService, object message, string category)
        {
            var logEntry = new LogEntry {Message = message.ToString()};
            logEntry.Categories.Add(category);
            loggingService.Write(logEntry);
        }

        public static void Write(this ILoggingService loggingService, object message, string category, int priority)
        {
            var logEntry = new LogEntry
                               {
                                   Message = message.ToString(),
                                   Priority = priority
                               };
            logEntry.Categories.Add(category);
            loggingService.Write(logEntry);
        }

        public static void Write(this ILoggingService loggingService, object message, string category, int priority,
                                 int eventId)
        {
            var logEntry = new LogEntry
                               {
                                   Message = message.ToString(),
                                   Priority = priority,
                                   EventId = eventId
                               };
            logEntry.Categories.Add(category);
            loggingService.Write(logEntry);
        }

        public static void Write(this ILoggingService loggingService, object message, string category, int priority,
                                 int eventId, TraceEventType severity)
        {
            var logEntry = new LogEntry
                               {
                                   Message = message.ToString(),
                                   Priority = priority,
                                   EventId = eventId,
                                   Severity = severity
                               };
            logEntry.Categories.Add(category);
            loggingService.Write(logEntry);
        }

        public static void Write(this ILoggingService loggingService, object message, string category, int priority,
                                 int eventId, TraceEventType severity, string title)
        {
            var logEntry = new LogEntry
                               {
                                   Message = message.ToString(),
                                   Priority = priority,
                                   EventId = eventId,
                                   Severity = severity,
                                   Title = title
                               };
            logEntry.Categories.Add(category);
            loggingService.Write(logEntry);
        }

        public static void Write(this ILoggingService loggingService, object message,
                                 IDictionary<string, object> properties)
        {
            var logEntry = new LogEntry
                               {
                                   Message = message.ToString(),
                                   ExtendedProperties = properties
                               };
            loggingService.Write(logEntry);
        }

        public static void Write(this ILoggingService loggingService, object message, string category,
                                 IDictionary<string, object> properties)
        {
            var logEntry = new LogEntry
                               {
                                   Message = message.ToString(),
                                   ExtendedProperties = properties
                               };
            logEntry.Categories.Add(category);
            loggingService.Write(logEntry);
        }

        public static void Write(this ILoggingService loggingService, object message, string category, int priority,
                                 IDictionary<string, object> properties)
        {
            var logEntry = new LogEntry
                               {
                                   Message = message.ToString(),
                                   Priority = priority,
                                   ExtendedProperties = properties
                               };
            logEntry.Categories.Add(category);
            loggingService.Write(logEntry);
        }

        public static void Write(this ILoggingService loggingService, object message, string category, int priority,
                                 int eventId, TraceEventType severity, string title,
                                 IDictionary<string, object> properties)
        {
            var logEntry = new LogEntry
                               {
                                   Message = message.ToString(),
                                   Priority = priority,
                                   EventId = eventId,
                                   Severity = severity,
                                   Title = title,
                                   ExtendedProperties = properties
                               };
            logEntry.Categories.Add(category);
            loggingService.Write(logEntry);
        }

        public static void Write(this ILoggingService loggingService, object message, ICollection<string> categories)
        {
            var logEntry = new LogEntry
                               {
                                   Message = message.ToString(),
                                   Categories = categories
                               };
            loggingService.Write(logEntry);
        }

        public static void Write(this ILoggingService loggingService, object message, ICollection<string> categories,
                                 int priority)
        {
            var logEntry = new LogEntry
                               {
                                   Message = message.ToString(),
                                   Categories = categories,
                                   Priority = priority
                               };
            loggingService.Write(logEntry);
        }

        public static void Write(this ILoggingService loggingService, object message, ICollection<string> categories,
                                 int priority, int eventId)
        {
            var logEntry = new LogEntry
                               {
                                   Message = message.ToString(),
                                   Categories = categories,
                                   Priority = priority,
                                   EventId = eventId
                               };
            loggingService.Write(logEntry);
        }

        public static void Write(this ILoggingService loggingService, object message, ICollection<string> categories,
                                 int priority, int eventId, TraceEventType severity)
        {
            var logEntry = new LogEntry
                               {
                                   Message = message.ToString(),
                                   Categories = categories,
                                   Priority = priority,
                                   EventId = eventId,
                                   Severity = severity
                               };
            loggingService.Write(logEntry);
        }

        public static void Write(this ILoggingService loggingService, object message, ICollection<string> categories,
                                 int priority, int eventId, TraceEventType severity, string title)
        {
            var logEntry = new LogEntry
                               {
                                   Message = message.ToString(), 
                                   Categories = categories,
                                   Priority = priority,
                                   EventId = eventId,
                                   Severity = severity,
                                   Title = title
                               };
            loggingService.Write(logEntry);
        }

        public static void Write(this ILoggingService loggingService, object message, ICollection<string> categories,
                                 IDictionary<string, object> properties)
        {
            var logEntry = new LogEntry
                               {
                                   Message = message.ToString(),
                                   Categories = categories,
                                   ExtendedProperties = properties
                               };
            loggingService.Write(logEntry);
        }

        public static void Write(this ILoggingService loggingService, object message, ICollection<string> categories,
                                 int priority, IDictionary<string, object> properties)
        {
            var logEntry = new LogEntry
                               {
                                   Message = message.ToString(),
                                   Categories = categories,
                                   Priority = priority,
                                   ExtendedProperties = properties
                               };
            ;
            loggingService.Write(logEntry);
        }

        public static void Write(this ILoggingService loggingService, object message, ICollection<string> categories,
                                 int priority, int eventId, TraceEventType severity, string title,
                                 IDictionary<string, object> properties)
        {
            var logEntry = new LogEntry
                               {
                                   Message = message.ToString(),
                                   Categories = categories,
                                   Priority = priority,
                                   EventId = eventId,
                                   Severity = severity,
                                   Title = title,
                                   ExtendedProperties = properties
                               };
            loggingService.Write(logEntry);
        }

        public static void Write(this ILoggingService loggingService, object message, TraceEventType severity)
        {
            var logEntry = new LogEntry
                               {
                                   Message = message.ToString(), 
                                   Severity = severity
                               };
            loggingService.Write(logEntry);
        }

        public static void Write(this ILoggingService loggingService, object message, Exception ex)
        {
            var logEntry = new LogEntry
                               {
                                   Message = message.ToString(),
                                   Severity = TraceEventType.Error
                               };
            logEntry.ExtendedProperties["Exception"] = ex;
            loggingService.Write(logEntry);
        }
    }
}