using Machine.Specifications;
using NCommons.Testing.Equality;
using NCommons.Testing.Specs.TestTypes;

namespace NCommons.Testing.Specs.ExpectedObjectSpecs
{
    public class when_comparing_unequal_objects_with_string
    {
        static TypeWithString _actual;
        static TypeWithString _expected;

        static bool _result;

        Establish context = () =>
            {
                _actual = new TypeWithString { StringProperty = "test" };
                _expected = new TypeWithString { StringProperty = "test2" };
            };

        Because of = () => _result = _expected.ToExpectedObject().Equals(_actual);

        It should_be_equal = () => _result.ShouldBeFalse();
    }
}