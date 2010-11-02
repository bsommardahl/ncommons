using System.Collections.Generic;
using Machine.Specifications;
using NCommons.Testing.Equality;

namespace NCommons.Testing.Specs.ExpectedObjectSpecs
{
    public class when_comparing_unequal_dictionaries_of_string_string_with_different_count
    {
        static IDictionary<string, string> _actual;
        static IDictionary<string, string> _expected;

        static bool _result;

        Establish context = () =>
            {
                _actual = new Dictionary<string, string> { { "key1", "value1" }, {"key2", "value2"} };
                _expected = new Dictionary<string, string> { { "key2", "value2" } };
            };

        Because of = () => _result = _expected.ToExpectedObject().Equals(_actual);

        It should_not_be_equal = () => _result.ShouldBeFalse();
    }
}