using Machine.Specifications;
using NCommons.Testing.Equality;

namespace NCommons.Testing.Specs.ShouldWriterSpecs
{
    public class when_retrieving_formatted_result_from_unequal_comparision
    {
        static EqualityResult _result;
        static IWriter _writer;

        Establish context = () =>
            {
                _writer = new ShouldWriter();
                _result = new EqualityResult(false, "StringProperty", 1, 2);
            };

        Because of = () => _writer.Write(_result);

        It should_return_formatted_result =
            () => _writer.GetFormattedResults().ShouldEqual("For StringProperty, expected [1] but found [2]." + System.Environment.NewLine);
    }
}