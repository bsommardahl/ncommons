using Machine.Specifications;
using NCommons.Testing.Equality;

namespace NCommons.Testing.Specs.ShouldWriterSpecs
{
    public class when_retrieving_formatted_result_from_equal_comparision
    {
        static EqualityResult _result;
        static IWriter _writer;

        Establish context = () =>
            {
                _writer = new ShouldWriter();
                _result = new EqualityResult(true, "StringProperty", 1, 2);
            };

        Because of = () => _writer.Write(_result);

        It should_not_return_results =
            () => _writer.GetFormattedResults().ShouldBeEmpty();
    }
}