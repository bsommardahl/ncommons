using System;
using System.Diagnostics;
using Machine.Specifications;

namespace NCommons.Logging.Specs
{
    [Subject(typeof (LogEntry))]
    public class when_creating_a_log_entry_with_the_single_category_constructor
    {
        static Exception _exception;
        static LogEntry _logEntry;

        Establish context = () => { };

        Because of = () => _exception =
                           Catch.Exception(() => _logEntry = new LogEntry(
                                                                 "message", "category", 0, -1,
                                                                 TraceEventType.Verbose, "title", null));

        It should_succeed = () => _exception.ShouldBeNull();

        It should_add_category_to_collection = () => _logEntry.Categories.Contains("category").ShouldBeTrue();
    }
}