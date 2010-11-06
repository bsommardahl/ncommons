using System.Diagnostics;
using Machine.Specifications;

namespace NCommons.Logging.Specs
{
    [Subject(typeof (LogWriterMap<TraceEventType>))]
    public class when_mapping_a_log_entry
    {
        static LogEntry _logEntry;
        static LogWriterMapSpy _logWriterMapSpy;

        Establish context = () =>
            {
                _logEntry = new LogEntry {Severity = TraceEventType.Resume};
                _logWriterMapSpy = new LogWriterMapSpy();
            };

        Because of = () => _logWriterMapSpy.Write(_logEntry);

        It should_delegate_to_the_mapped_delegate = () => _logWriterMapSpy.WasDelegateCalled.ShouldBeTrue();
    }
}