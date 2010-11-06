using Machine.Specifications;
using NCommons.Testing.Equality;
using NCommons.Testing.Specs.TestTypes;

namespace NCommons.Testing.Specs.ExpectedObjectSpecs
{
    public class when_comparing_equal_objects_of_different_types_ignoring_types
    {
        static TypeWithString _expected;
        static TypeWithString2 _actual;
        static bool _result;

        Establish context = () =>
            {
                _expected = new TypeWithString()
                                {
                                    StringProperty = "this is a test"
                                };

                _actual = new TypeWithString2
                              {
                                  StringProperty = "this is a test"
                              };
            };

        Because of = () => _result = _expected.ToExpectedObject().IgnoreTypes().Equals(_actual);

        It should_be_equal = () => _result.ShouldBeTrue();
    }
}