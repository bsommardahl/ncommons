using Machine.Specifications;
using NCommons.Testing.Equality;
using NCommons.Testing.Specs.TestTypes;

namespace NCommons.Testing.Specs.ExpectedObjectSpecs
{
    public class when_comparing_equal_enums
    {
        static EnumType _actual;
        static EnumType _expected;

        static bool _result;

        Establish context = () =>
            {
                _actual = EnumType.Undefined;
                _expected = EnumType.Undefined;
            };

        Because of = () => _result = _expected.ToExpectedObject().Equals(_actual);

        It should_be_equal = () => _result.ShouldBeTrue();
    }
}