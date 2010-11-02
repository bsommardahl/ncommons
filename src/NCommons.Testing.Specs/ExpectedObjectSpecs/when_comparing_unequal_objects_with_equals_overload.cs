using Machine.Specifications;
using NCommons.Testing.Equality;
using NCommons.Testing.Specs.TestTypes;

namespace NCommons.Testing.Specs.ExpectedObjectSpecs
{
    public class when_comparing_unequal_objects_with_equals_overload
    {
        static EqualsOverrideType _actual;
        static EqualsOverrideType _expected;

        static bool _result;

        Establish context = () =>
            {
                _actual = new EqualsOverrideType(true);
                _expected = new EqualsOverrideType(false);
            };

        Because of = () => _result = _expected.ToExpectedObject().Equals(_actual);

        It should_not_be_equal = () => _result.ShouldBeFalse();
    }
}