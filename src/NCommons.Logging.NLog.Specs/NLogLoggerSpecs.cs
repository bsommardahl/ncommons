using System.Diagnostics;
using Machine.Specifications;
using Moq;
using It=Machine.Specifications.It;

namespace NCommons.Logging.NLog.Specs
{
    namespace NLogLoggerSpecs
    {
        [Subject(typeof(NLogLogger))]
        public class when_writing_a_log_entry
        {
            static LogEntry _logEntry;
            static NLogLogger _logger;
            static Mock<ILogWriter> _mockLogWriter;

            Establish context = () =>
                {
                    _logEntry = new LogEntry { Severity = TraceEventType.Warning };
                    _mockLogWriter = new Mock<ILogWriter>();
                    _logger = new NLogLogger(_mockLogWriter.Object);
                };

            Because of = () => _logger.Write(_logEntry);

            It should_delegate_to_the_log_writer = () => _mockLogWriter.Verify(x => x.Write(_logEntry));
        }
    }
}