using Machine.Specifications;
using NCommons.Testing.Equality;
using NCommons.Testing.Specs.TestTypes;

namespace NCommons.Testing.Specs.ExpectedObjectSpecs
{
    public class when_comparing_objects_of_different_types_with_different_members
    {
        static TypeWithString _actual;
        static TypeWithDecimal _expected;
        static bool _result;

        Establish context = () =>
            {
                _expected = new TypeWithDecimal {DecimalProperty = 10.0m};

                _actual = new TypeWithString {StringProperty = "this is a test"};
            };

        Because of = () => _result = _expected.ToExpectedObject().Equals(_actual);

        It should_not_be_equal = () => _result.ShouldBeFalse();
    }
}