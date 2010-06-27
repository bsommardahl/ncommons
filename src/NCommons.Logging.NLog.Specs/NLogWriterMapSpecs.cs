using System.Diagnostics;
using Machine.Specifications;
using Moq;
using Moq.Protected;
using NLog;
using NLog.Config;
using It = Machine.Specifications.It;

namespace NCommons.Logging.NLog.Specs
{
    [Subject(typeof (NLogWriterMap))]
    public class when_writing_a_log_entry_with_default_trace_event_type : given_a_standard_nlog_logger_context
    {
        static LogEntry _logEntry;

        Establish context = () => _logEntry = new LogEntry();

        Because of = () => Map.Write(_logEntry);

        It should_call_the_target_with_a_info_logging_level_message = () => Level.ShouldEqual(LogLevel.Info);
    }

    [Subject(typeof(NLogWriterMap))]
    public class when_writing_a_log_entry_with_warn_trace_event_type : given_a_standard_nlog_logger_context
    {
        static LogEntry _logEntry;

        Establish context = () => _logEntry = new LogEntry { Severity = TraceEventType.Warning };

        Because of = () => Map.Write(_logEntry);

        It should_call_the_target_with_a_warning_logging_level_message = () => Level.ShouldEqual(LogLevel.Warn);
    }

    [Subject(typeof(NLogWriterMap))]
    public class when_writing_a_log_entry_with_verbose_trace_event_type : given_a_standard_nlog_logger_context
    {
        static LogEntry _logEntry;

        Establish context = () => _logEntry = new LogEntry { Severity = TraceEventType.Warning };

        Because of = () => Map.Write(_logEntry);

        It should_call_the_target_with_a_warning_logging_level_message = () => Level.ShouldEqual(LogLevel.Warn);
    }

    [Subject(typeof(NLogWriterMap))]
    public class when_writing_a_log_entry_with_transfer_trace_event_type : given_a_standard_nlog_logger_context
    {
        static LogEntry _logEntry;

        Establish context = () => _logEntry = new LogEntry { Severity = TraceEventType.Transfer };

        Because of = () => Map.Write(_logEntry);

        It should_call_the_target_with_a_warning_logging_level_message = () => Level.ShouldEqual(LogLevel.Debug);
    }

    [Subject(typeof(NLogWriterMap))]
    public class when_writing_a_log_entry_with_suspend_trace_event_type : given_a_standard_nlog_logger_context
    {
        static LogEntry _logEntry;

        Establish context = () => _logEntry = new LogEntry { Severity = TraceEventType.Suspend };

        Because of = () => Map.Write(_logEntry);

        It should_call_the_target_with_a_warning_logging_level_message = () => Level.ShouldEqual(LogLevel.Debug);
    }

    [Subject(typeof(NLogWriterMap))]
    public class when_writing_a_log_entry_with_start_trace_event_type : given_a_standard_nlog_logger_context
    {
        static LogEntry _logEntry;

        Establish context = () => _logEntry = new LogEntry { Severity = TraceEventType.Start };

        Because of = () => Map.Write(_logEntry);

        It should_call_the_target_with_a_warning_logging_level_message = () => Level.ShouldEqual(LogLevel.Debug);
    }

    [Subject(typeof(NLogWriterMap))]
    public class when_writing_a_log_entry_with_stop_trace_event_type : given_a_standard_nlog_logger_context
    {
        static LogEntry _logEntry;

        Establish context = () => _logEntry = new LogEntry { Severity = TraceEventType.Stop };

        Because of = () => Map.Write(_logEntry);

        It should_call_the_target_with_a_warning_logging_level_message = () => Level.ShouldEqual(LogLevel.Debug);
    }

    [Subject(typeof(NLogWriterMap))]
    public class when_writing_a_log_entry_with_resume_trace_event_type : given_a_standard_nlog_logger_context
    {
        static LogEntry _logEntry;

        Establish context = () => _logEntry = new LogEntry { Severity = TraceEventType.Resume };

        Because of = () => Map.Write(_logEntry);

        It should_call_the_target_with_a_warning_logging_level_message = () => Level.ShouldEqual(LogLevel.Debug);
    }

    [Subject(typeof(NLogWriterMap))]
    public class when_writing_a_log_entry_with_information_trace_event_type : given_a_standard_nlog_logger_context
    {
        static LogEntry _logEntry;

        Establish context = () => _logEntry = new LogEntry { Severity = TraceEventType.Information };

        Because of = () => Map.Write(_logEntry);

        It should_call_the_target_with_a_information_logging_level_message = () => Level.ShouldEqual(LogLevel.Info);
    }

    [Subject(typeof(NLogWriterMap))]
    public class when_writing_a_log_entry_with_error_trace_event_type : given_a_standard_nlog_logger_context
    {
        static LogEntry _logEntry;

        Establish context = () => _logEntry = new LogEntry { Severity = TraceEventType.Error };

        Because of = () => Map.Write(_logEntry);

        It should_call_the_target_with_a_error_logging_level_message = () => Level.ShouldEqual(LogLevel.Error);
    }

    [Subject(typeof(NLogWriterMap))]
    public class when_writing_a_log_entry_with_critical_trace_event_type : given_a_standard_nlog_logger_context
    {
        static LogEntry _logEntry;

        Establish context = () => _logEntry = new LogEntry { Severity = TraceEventType.Critical };

        Because of = () => Map.Write(_logEntry);

        It should_call_the_target_with_a_fatal_logging_level_message = () => Level.ShouldEqual(LogLevel.Fatal);
    }

    public abstract class given_a_standard_nlog_logger_context
    {
        protected static LogLevel Level;
        protected static Logger Logger;
        protected static NLogWriterMap Map;
        protected static Mock<Target> MockTarget;

        Establish context = () =>
            {
                var config = new LoggingConfiguration();
                MockTarget = new Mock<Target>();
                config.AddTarget("mockTarget", MockTarget.Object);
                var rule = new LoggingRule("*", LogLevel.Debug, MockTarget.Object);
                config.LoggingRules.Add(rule);
                LogManager.Configuration = config;
                Logger = LogManager.GetLogger("Example");
                MockTarget.Protected()
                    .Setup("Write", ItExpr.IsAny<LogEventInfo>())
                    .Callback<LogEventInfo>(l => Level = l.Level);

                Map = new NLogWriterMap(Logger);
            };
    }
}