using System;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace NCommons.Logging.Specs
{
    [Subject(typeof (LoggingService))]
    public class when_writing_a_log_entry : given_a_logging_service
    {
        static LogEntry _logEntry;

        Establish context = () => _logEntry = new LogEntry();

        Because of = () => LoggingService.Write(_logEntry);

        It should_delegate_to_the_logger_impl = () => MockLogger.Verify(x => x.Write(_logEntry));
    }

    [Subject(typeof (LoggingService))]
    public class when_adding_a_new_logger : given_a_logging_service
    {
        static Mock<ILogger> _mockLogger;
        static LogEntry _logEntry;

        Establish context = () =>
            {
                _logEntry = new LogEntry();
                _mockLogger = new Mock<ILogger>();
                LoggingService.AddLogger(_mockLogger.Object);
            };

        Because of = () => LoggingService.Write(_logEntry);

        It should_write_to_the_logger = () => _mockLogger.Verify(x => x.Write(_logEntry));
    }

    [Subject(typeof (LoggingService))]
    public class when_removing_a_logger : given_a_logging_service
    {
        static Mock<ILogger> _mockLogger;
        static LogEntry _logEntry;

        Establish context = () =>
            {
                _logEntry = new LogEntry();
                _mockLogger = new Mock<ILogger>();
                LoggingService.RemoveLogger(MockLogger.Object);
            };

        Because of = () => LoggingService.Write(_logEntry);

        It should_not_write_to_the_removed_logger = () => _mockLogger.Verify(x => x.Write(_logEntry), Times.Never());
    }

    [Subject(typeof (LoggingService))]
    public class when_attempting_to_add_an_existing_logger : given_a_logging_service
    {
        static Exception _exception;
        Establish context = () => { };

        Because of = () => _exception = Catch.Exception(() => LoggingService.AddLogger(MockLogger.Object));

        It should_throw_an_exception = () => _exception.ShouldNotBeNull();
    }

    public abstract class given_a_logging_service
    {
        protected static LogEntry LogEntry;
        protected static ILoggingService LoggingService;
        protected static Mock<ILogger> MockLogger;

        Establish context = () =>
            {
                MockLogger = new Mock<ILogger>();
                LogEntry = new LogEntry();
                LoggingService = new LoggingService(MockLogger.Object);
            };
    }
}