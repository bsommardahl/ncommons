using System.Diagnostics;
using log4net.Appender;
using log4net.Core;
using log4net.Repository;
using Machine.Specifications;
using Moq;
using It=Machine.Specifications.It;

namespace NCommons.Logging.Log4Net.Specs
{
    namespace Log4NetLoggerSpecs
    {
        [Subject(typeof (Log4NetLogger))]
        public class when_writing_a_log_entry
        {
            static LogEntry _logEntry;
            static Log4NetLogger _logger;
            static Mock<ILogWriter> _mockLogWriter;

            Establish context = () =>
                {
                    _logEntry = new LogEntry {Severity = TraceEventType.Warning};
                    _mockLogWriter = new Mock<ILogWriter>();
                    _logger = new Log4NetLogger(_mockLogWriter.Object);
                };

            Because of = () => _logger.Write(_logEntry);

            It should_delegate_to_the_log_writer = () => _mockLogWriter.Verify(x => x.Write(_logEntry));
        }
    }
}