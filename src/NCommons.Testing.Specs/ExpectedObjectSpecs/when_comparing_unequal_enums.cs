using Machine.Specifications;
using NCommons.Testing.Equality;
using NCommons.Testing.Specs.TestTypes;

namespace NCommons.Testing.Specs.ExpectedObjectSpecs
{
    public class when_comparing_unequal_enums
    {
        static EnumType _actual;
        static EnumType _expected;

        static bool _result;

        Establish context = () =>
            {
                _actual = EnumType.Undefined;
                _expected = EnumType.Value1;
            };

        Because of = () => _result = _expected.ToExpectedObject().Equals(_actual);

        It should_not_be_equal = () => _result.ShouldBeFalse();
    }
}