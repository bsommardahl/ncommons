using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace NCommons.Logging.Specs
{
    [Subject(typeof (LoggingServiceExtensions))]
    public class when_writing_with_message : given_a_logging_service_extensions_context
    {
        Because of = () => MockLoggingService.Object.Write(LogEntry.Message);

        It should_invoke_service_with_message =
            () => CapturedLogEntry.ShouldEqual(new LogEntry {Message = LogEntry.Message});
    }

    [Subject(typeof (LoggingServiceExtensions))]
    public class when_writing_with_message_category : given_a_logging_service_extensions_context
    {
        Because of = () => MockLoggingService.Object.Write(LogEntry.Message, LogEntry.Categories.First());

        It should_invoke_service_with_matching_log_entry =
            () => CapturedLogEntry.ShouldEqual(new LogEntry

                                                   {
                                                       Message = LogEntry.Message,
                                                       Categories = LogEntry.Categories
                                                   });
    }

    [Subject(typeof (LoggingServiceExtensions))]
    public class when_writing_with_message_category_priority : given_a_logging_service_extensions_context
    {
        Because of = () =>
                     MockLoggingService.Object.Write(LogEntry.Message, LogEntry.Categories.First(), LogEntry.Priority);

        It should_invoke_service_with_matching_log_entry =
            () => CapturedLogEntry.ShouldEqual(new LogEntry
                                                   {
                                                       Message = LogEntry.Message,
                                                       Categories = LogEntry.Categories,
                                                       Priority = LogEntry.Priority
                                                   });
    }

    [Subject(typeof (LoggingServiceExtensions))]
    public class when_writing_with_message_category_priority_eventId : given_a_logging_service_extensions_context
    {
        Because of = () =>
                     MockLoggingService.Object.Write(LogEntry.Message, LogEntry.Categories.First(), LogEntry.Priority,
                                                     LogEntry.EventId);

        It should_invoke_service_with_matching_log_entry =
            () => CapturedLogEntry.ShouldEqual(new LogEntry
                                                   {
                                                       Message = LogEntry.Message,
                                                       Categories = LogEntry.Categories,
                                                       Priority = LogEntry.Priority,
                                                       EventId = LogEntry.EventId
                                                   });
    }

    [Subject(typeof (LoggingServiceExtensions))]
    public class when_writing_with_message_category_priority_eventId_severity :
        given_a_logging_service_extensions_context
    {
        Because of = () =>
                     MockLoggingService.Object.Write(LogEntry.Message, LogEntry.Categories.First(), LogEntry.Priority,
                                                     LogEntry.EventId, LogEntry.Severity);

        It should_invoke_service_with_matching_log_entry =
            () => CapturedLogEntry.ShouldEqual(new LogEntry
                                                   {
                                                       Message = LogEntry.Message,
                                                       Categories = LogEntry.Categories,
                                                       Priority = LogEntry.Priority,
                                                       EventId = LogEntry.EventId,
                                                       Severity = LogEntry.Severity
                                                   });
    }

    [Subject(typeof (LoggingServiceExtensions))]
    public class when_writing_with_message_category_priority_eventId_severity_title :
        given_a_logging_service_extensions_context
    {
        Because of = () =>
                     MockLoggingService.Object.Write(LogEntry.Message, LogEntry.Categories.First(), LogEntry.Priority,
                                                     LogEntry.EventId, LogEntry.Severity, LogEntry.Title);

        It should_invoke_service_with_matching_log_entry =
            () => CapturedLogEntry.ShouldEqual(new LogEntry
                                                   {
                                                       Message = LogEntry.Message,
                                                       Categories = new[] {LogEntry.Categories.First()},
                                                       Priority = LogEntry.Priority,
                                                       EventId = LogEntry.EventId,
                                                       Severity = LogEntry.Severity,
                                                       Title = LogEntry.Title
                                                   });
    }

    [Subject(typeof (LoggingServiceExtensions))]
    public class when_writing_with_message_properties :
        given_a_logging_service_extensions_context
    {
        Because of = () =>
                     MockLoggingService.Object.Write(LogEntry.Message, LogEntry.ExtendedProperties);

        It should_invoke_service_with_matching_log_entry =
            () => CapturedLogEntry.ShouldEqual(new LogEntry
                                                   {
                                                       Message = LogEntry.Message,
                                                       ExtendedProperties = LogEntry.ExtendedProperties
                                                   });
    }

    [Subject(typeof (LoggingServiceExtensions))]
    public class when_writing_with_message_category_properties :
        given_a_logging_service_extensions_context
    {
        Because of = () =>
                     MockLoggingService.Object.Write(LogEntry.Message, LogEntry.Categories.First(),
                                                     LogEntry.ExtendedProperties);

        It should_invoke_service_with_matching_log_entry =
            () => CapturedLogEntry.ShouldEqual(new LogEntry
                                                   {
                                                       Message = LogEntry.Message,
                                                       Categories = new[] {LogEntry.Categories.First()},
                                                       ExtendedProperties = LogEntry.ExtendedProperties
                                                   });
    }

    [Subject(typeof (LoggingServiceExtensions))]
    public class when_writing_with_message_category_priority_properties :
        given_a_logging_service_extensions_context
    {
        Because of = () =>
                     MockLoggingService.Object.Write(LogEntry.Message, LogEntry.Categories.First(), LogEntry.Priority,
                                                     LogEntry.ExtendedProperties);

        It should_invoke_service_with_matching_log_entry =
            () => CapturedLogEntry.ShouldEqual(new LogEntry
                                                   {
                                                       Message = LogEntry.Message,
                                                       Categories = new[] {LogEntry.Categories.First()},
                                                       Priority = LogEntry.Priority,
                                                       ExtendedProperties = LogEntry.ExtendedProperties
                                                   });
    }

    [Subject(typeof (LoggingServiceExtensions))]
    public class when_writing_with_message_category_priority_eventId_severity_title_properties :
        given_a_logging_service_extensions_context
    {
        Because of = () =>
                     MockLoggingService.Object.Write(LogEntry.Message, LogEntry.Categories.First(), LogEntry.Priority,
                                                     LogEntry.EventId, LogEntry.Severity, LogEntry.Title,
                                                     LogEntry.ExtendedProperties);

        It should_invoke_service_with_matching_log_entry =
            () => CapturedLogEntry.ShouldEqual(new LogEntry
                                                   {
                                                       Message = LogEntry.Message,
                                                       Categories = new[] {LogEntry.Categories.First()},
                                                       Priority = LogEntry.Priority,
                                                       EventId = LogEntry.EventId,
                                                       Severity = LogEntry.Severity,
                                                       Title = LogEntry.Title,
                                                       ExtendedProperties = LogEntry.ExtendedProperties
                                                   });
    }

    [Subject(typeof (LoggingServiceExtensions))]
    public class when_writing_with_message_categories :
        given_a_logging_service_extensions_context
    {
        Because of = () =>
                     MockLoggingService.Object.Write(LogEntry.Message, LogEntry.Categories);

        It should_invoke_service_with_matching_log_entry =
            () => CapturedLogEntry.ShouldEqual(new LogEntry
                                                   {
                                                       Message = LogEntry.Message,
                                                       Categories = LogEntry.Categories
                                                   });
    }

    [Subject(typeof (LoggingServiceExtensions))]
    public class when_writing_with_message_categories_priority :
        given_a_logging_service_extensions_context
    {
        Because of = () =>
                     MockLoggingService.Object.Write(LogEntry.Message, LogEntry.Categories, LogEntry.Priority);

        It should_invoke_service_with_matching_log_entry =
            () => CapturedLogEntry.ShouldEqual(new LogEntry
                                                   {
                                                       Message = LogEntry.Message,
                                                       Categories = LogEntry.Categories,
                                                       Priority = LogEntry.Priority
                                                   });
    }

    [Subject(typeof (LoggingServiceExtensions))]
    public class when_writing_with_message_categories_priority_eventId :
        given_a_logging_service_extensions_context
    {
        Because of = () =>
                     MockLoggingService.Object.Write(LogEntry.Message, LogEntry.Categories, LogEntry.Priority,
                                                     LogEntry.EventId);

        It should_invoke_service_with_matching_log_entry =
            () => CapturedLogEntry.ShouldEqual(new LogEntry
                                                   {
                                                       Message = LogEntry.Message,
                                                       Categories = LogEntry.Categories,
                                                       Priority = LogEntry.Priority,
                                                       EventId = LogEntry.EventId
                                                   });
    }

    [Subject(typeof (LoggingServiceExtensions))]
    public class when_writing_with_message_categories_priority_eventId_severity :
        given_a_logging_service_extensions_context
    {
        Because of = () =>
                     MockLoggingService.Object.Write(LogEntry.Message, LogEntry.Categories, LogEntry.Priority,
                                                     LogEntry.EventId, LogEntry.Severity);

        It should_invoke_service_with_matching_log_entry =
            () => CapturedLogEntry.ShouldEqual(new LogEntry
                                                   {
                                                       Message = LogEntry.Message,
                                                       Categories = LogEntry.Categories,
                                                       Priority = LogEntry.Priority,
                                                       EventId = LogEntry.EventId,
                                                       Severity = LogEntry.Severity
                                                   });
    }

    [Subject(typeof (LoggingServiceExtensions))]
    public class when_writing_with_message_categories_priority_eventId_severity_title :
        given_a_logging_service_extensions_context
    {
        Because of = () =>
                     MockLoggingService.Object.Write(LogEntry.Message, LogEntry.Categories, LogEntry.Priority,
                                                     LogEntry.EventId, LogEntry.Severity, LogEntry.Title);

        It should_invoke_service_with_matching_log_entry =
            () => CapturedLogEntry.ShouldEqual(new LogEntry
                                                   {
                                                       Message = LogEntry.Message,
                                                       Categories = LogEntry.Categories,
                                                       Priority = LogEntry.Priority,
                                                       EventId = LogEntry.EventId,
                                                       Severity = LogEntry.Severity,
                                                       Title = LogEntry.Title
                                                   });
    }

    [Subject(typeof (LoggingServiceExtensions))]
    public class when_writing_with_message_categories_properties :
        given_a_logging_service_extensions_context
    {
        Because of = () =>
                     MockLoggingService.Object.Write(LogEntry.Message, LogEntry.Categories, LogEntry.ExtendedProperties);

        It should_invoke_service_with_matching_log_entry =
            () => CapturedLogEntry.ShouldEqual(new LogEntry
                                                   {
                                                       Message = LogEntry.Message,
                                                       Categories = LogEntry.Categories,
                                                       ExtendedProperties = LogEntry.ExtendedProperties
                                                   });
    }

    [Subject(typeof (LoggingServiceExtensions))]
    public class when_writing_with_message_categories_priority_properties :
        given_a_logging_service_extensions_context
    {
        Because of = () =>
                     MockLoggingService.Object.Write(LogEntry.Message, LogEntry.Categories, LogEntry.Priority,
                                                     LogEntry.ExtendedProperties);

        It should_invoke_service_with_matching_log_entry =
            () => CapturedLogEntry.ShouldEqual(new LogEntry
                                                   {
                                                       Message = LogEntry.Message,
                                                       Categories = LogEntry.Categories,
                                                       Priority = LogEntry.Priority,
                                                       ExtendedProperties = LogEntry.ExtendedProperties
                                                   });
    }

    [Subject(typeof (LoggingServiceExtensions))]
    public class when_writing_with_message_categories_priority_eventId_severity_title_properties :
        given_a_logging_service_extensions_context
    {
        Because of = () =>
                     MockLoggingService.Object.Write(LogEntry.Message, LogEntry.Categories, LogEntry.Priority,
                                                     LogEntry.EventId, LogEntry.Severity, LogEntry.Title,
                                                     LogEntry.ExtendedProperties);

        It should_invoke_service_with_matching_log_entry =
            () => CapturedLogEntry.ShouldEqual(new LogEntry
                                                   {
                                                       Message = LogEntry.Message,
                                                       Categories = LogEntry.Categories,
                                                       Priority = LogEntry.Priority,
                                                       EventId = LogEntry.EventId,
                                                       Severity = LogEntry.Severity,
                                                       Title = LogEntry.Title,
                                                       ExtendedProperties = LogEntry.ExtendedProperties
                                                   });
    }

    [Subject(typeof (LoggingServiceExtensions))]
    public class when_writing_with_message_severity :
        given_a_logging_service_extensions_context
    {
        Because of = () =>
                     MockLoggingService.Object.Write(LogEntry.Message, LogEntry.Severity);

        It should_invoke_service_with_matching_log_entry =
            () => CapturedLogEntry.ShouldEqual(new LogEntry
                                                   {
                                                       Message = LogEntry.Message,
                                                       Severity = LogEntry.Severity
                                                   });
    }

    [Subject(typeof (LoggingServiceExtensions))]
    public class when_writing_with_message_exception :
        given_a_logging_service_extensions_context
    {
        Because of = () =>
                     MockLoggingService.Object.Write(LogEntry.Message,
                                                     (Exception) LogEntry.ExtendedProperties["Exception"]);

        It should_invoke_service_with_matching_log_entry =
            () => CapturedLogEntry.ShouldEqual(new LogEntry
                                                   {
                                                       Message = LogEntry.Message,
                                                       ExtendedProperties =
                                                           new Dictionary<string, object>
                                                               {
                                                                   {
                                                                       "Exception",
                                                                       LogEntry.ExtendedProperties["Exception"]
                                                                       }
                                                               },
                                                       Severity = TraceEventType.Error
                                                   });
    }


    public static class LogEntryTestExtensions
    {
        public static void ShouldEqual(this LogEntry logEntry, LogEntry other)
        {
            var ex = new Exception();

            if (ReferenceEquals(null, other)) throw ex;
            if (ReferenceEquals(logEntry, other)) return;

            if (!other.ActivityId.Equals(logEntry.ActivityId))
                throw ex;
            if (!Equals(other.AppDomainName, logEntry.AppDomainName))
                throw ex;

            List<string> otherCategories = other.Categories.ToList();
            List<string> categories = logEntry.Categories.ToList();
            for (int i = 0; i < otherCategories.Count; i++)
            {
                if (otherCategories[i] != categories[i])
                    throw ex;
            }

            ICollection<string> otherPropertyKeys = other.ExtendedProperties.Keys;

            foreach (string key in otherPropertyKeys)
            {
                if (other.ExtendedProperties[key] != logEntry.ExtendedProperties[key])
                    throw ex;
            }

            if (other.EventId != logEntry.EventId)
                throw ex;
            if (!Equals(other.MachineName, logEntry.MachineName))
                throw ex;
            if (!Equals(other.ManagedThreadName, logEntry.ManagedThreadName))
                throw ex;
            if (!Equals(other.Message, logEntry.Message))
                throw ex;
            if (other.Priority != logEntry.Priority)
                throw ex;
            if (!Equals(other.ProcessId, logEntry.ProcessId))
                throw ex;
            if (!Equals(other.ProcessName, logEntry.ProcessName))
                throw ex;
            if (!other.RelatedActivityId.Equals(logEntry.RelatedActivityId))
                throw ex;
            if (!Equals(other.Severity, logEntry.Severity))
                throw ex;
            if (!Equals(other.Title, logEntry.Title))
                throw ex;
        }
    }

    public abstract class given_a_logging_service_extensions_context
    {
        protected static LogEntry CapturedLogEntry;
        protected static LogEntry LogEntry;
        protected static Mock<ILoggingService> MockLoggingService;

        Establish context = () =>
            {
                LogEntry = new LogEntry
                               {
                                   Message = "test message",
                                   Categories = new[] {"test category"},
                                   ActivityId = Guid.NewGuid(),
                                   AppDomainName = "test app domain",
                                   EventId = 42,
                                   ExtendedProperties = new Dictionary<string, object>
                                                            {
                                                                {"key1", new object()},
                                                                {"Exception", new Exception("test")}
                                                            },
                                   MachineName = "test machine",
                                   ManagedThreadName = "test thread name",
                                   Priority = 30,
                                   ProcessId = "20",
                                   ProcessName = "process name",
                                   RelatedActivityId = Guid.NewGuid(),
                                   Severity = TraceEventType.Start,
                                   Title = "test title",
                                   Win32ThreadId = "222222"
                               };

                MockLoggingService = new Mock<ILoggingService>();
                MockLoggingService.Setup(s => s.Write(Moq.It.IsAny<LogEntry>()))
                    .Callback<LogEntry>(e => CapturedLogEntry = e);
            };
    }
}