using Machine.Specifications;
using NCommons.Testing.Equality;

namespace NCommons.Testing.Specs.ExpectedObjectSpecs
{
    public class when_comparing_nulls
    {
        static bool _result;

        Because of = () => _result = ((object) null).ToExpectedObject().Equals((object) null);

        It should_be_equal = () => _result.ShouldBeTrue();
    }
}