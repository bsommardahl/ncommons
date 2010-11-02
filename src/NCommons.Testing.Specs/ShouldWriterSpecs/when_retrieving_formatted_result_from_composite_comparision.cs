using Machine.Specifications;
using NCommons.Testing.Equality;

namespace NCommons.Testing.Specs.ShouldWriterSpecs
{
    public class when_retrieving_formatted_result_from_composite_comparision
    {
        static EqualityResult _result;
        static string _results;
        static IWriter _writer;

        Establish context = () =>
            {
                _writer = new ShouldWriter();
                _writer.Write(new EqualityResult(false, "ContainingObject.StringProperty", 1, 2));
                _writer.Write(new EqualityResult(false, "ContainingObject", 1, 2));
            };

        Because of = () => _results = _writer.GetFormattedResults();

        It should_not_contain_error_for_composing_object = () => _results.ShouldNotContain("ContainingObject:");
    }
}