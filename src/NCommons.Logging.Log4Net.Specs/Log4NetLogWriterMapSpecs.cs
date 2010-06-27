using System.Diagnostics;
using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Repository;
using Machine.Specifications;
using Moq;
using It=Machine.Specifications.It;

namespace NCommons.Logging.Log4Net.Specs
{
    namespace Log4NetLogWriterMapSpecs
    {
        [Subject(typeof (Log4NetLogWriterMap))]
        public class when_writing_a_log_entry
        {
            static LogEntry _logEntry;
            static Log4NetLogWriterMap _map;
            static Mock<ILog> _mockLog;

            Establish context = () =>
                {
                    _mockLog = new Mock<ILog>();
                    _logEntry = new LogEntry {Severity = TraceEventType.Warning};
                    _map = new Log4NetLogWriterMap(_mockLog.Object);
                };

            Because of = () => _map.Write(_logEntry);

            It should_call_the_log = () => _mockLog.Verify(x => x.Warn(string.Empty, null));
        }

        [Tags("integration")]
        [Subject(typeof (Log4NetLogWriterMap))]
        public class when_writing_with_a_real_logger
        {
            static LogEntry _logEntry;
            static Log4NetLogWriterMap _logWriter;
            static Mock<IAppender> _mockAppender;

            Establish context = () =>
                {
                    _logEntry = new LogEntry {Severity = TraceEventType.Warning};

                    _mockAppender = new Mock<IAppender>();

                    var log = LogManager.GetLogger("Default");
                    var configurator = log.Logger.Repository as IBasicRepositoryConfigurator;
                    if (configurator != null) configurator.Configure(_mockAppender.Object);
                    _logWriter = new Log4NetLogWriterMap(LogManager.GetLogger("Default"));
                };

            Because of = () => _logWriter.Write(_logEntry);

            It should_call_the_appender = () => _mockAppender.Verify(x => x.DoAppend(Moq.It.IsAny<LoggingEvent>()));
        }
    }
}