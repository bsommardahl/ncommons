using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace NCommons.Logging.EnterpriseLibrary.Specs
{
    namespace EnterpriseLibraryLoggerSpecs
    {
        [Tags("integration")]
        [Subject(typeof (EnterpriseLibraryLogger))]
        public class when_writing_a_log_entry
        {
            static LogEntry _logEntry;
            static EnterpriseLibraryLogger _logger;

            Cleanup after = TraceListenerSpy.Reset;

            Establish context = () =>
                {
                    _logEntry = new LogEntry {Message = "test"};
                    _logger = new EnterpriseLibraryLogger(new ConfigurationSourceStub());
                };

            Because of = () => _logger.Write(_logEntry);

            It should_delegate_to_the_trace_listener = () => TraceListenerSpy.Message.ShouldEqual("test");
        }

        [Subject(typeof (EnterpriseLibraryLogger))]
        public class when_writing_a_log_entry_with_existing_log_writer
        {
            static LogEntry _logEntry;
            static EnterpriseLibraryLogger _logger;
            static Mock<ILogWriter> _mockLogWriter;

            Establish context = () =>
                {
                    _mockLogWriter = new Mock<ILogWriter>();
                    _logEntry = new LogEntry {Message = "test"};
                    _logger = new EnterpriseLibraryLogger(_mockLogWriter.Object);
                };

            Because of = () => _logger.Write(_logEntry);

            It should_delegate_to_the_log_writer = () => _mockLogWriter.Verify(x => x.Write(_logEntry));
        }
    }
}