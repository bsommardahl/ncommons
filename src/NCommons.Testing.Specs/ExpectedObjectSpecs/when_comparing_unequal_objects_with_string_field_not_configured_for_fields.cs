using Machine.Specifications;
using NCommons.Testing.Equality;
using NCommons.Testing.Specs.TestTypes;

namespace NCommons.Testing.Specs.ExpectedObjectSpecs
{
    public class when_comparing_unequal_objects_with_string_field_not_configured_for_fields
    {
        static TypeWithStringField _actual;
        static ExpectedObject _expected;
        static bool _result;

        Establish context = () =>
            {
                _expected = new TypeWithStringField {StringField = "test"}.ToExpectedObject();
                _actual = new TypeWithStringField { StringField = "test2" };
            };

        Because of = () => _result = _expected.Equals(_actual);

        It should_not_be_equal = () => _result.ShouldBeFalse();
    }
}