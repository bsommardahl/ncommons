using System.Collections.Generic;
using Machine.Specifications;
using NCommons.Testing.Equality;

namespace NCommons.Testing.Specs.ExpectedObjectSpecs
{
    public class when_comparing_equal_dictionaries_of_string_string
    {
        static IDictionary<string, string> _actual;
        static IDictionary<string, string> _expected;

        static bool _result;

        Establish context = () =>
            {
                _actual = new Dictionary<string, string> {{"key1", "value1"}};
                _expected = new Dictionary<string, string> { { "key1", "value1" } };
            };

        Because of = () => _result = _expected.ToExpectedObject().Equals(_actual);

        It should_be_equal = () => _result.ShouldBeTrue();
    }
}