using System.Collections.Generic;
using Machine.Specifications;
using NCommons.Testing.Equality;
using NCommons.Testing.Specs.TestTypes;

namespace NCommons.Testing.Specs.ExpectedObjectSpecs
{
    public class when_comparing_unequal_objects_with_ienumerable
    {
        static TypeWithIEnumerable _actual;
        static TypeWithIEnumerable _expected;

        static bool _result;

        Establish context = () =>
            {
                _expected = new TypeWithIEnumerable {Objects = new List<string> {"test1", "test2"}};
                _actual = new TypeWithIEnumerable {Objects = new List<string> {"test3", "test4"}};
            };

        Because of = () => _result = _expected.ToExpectedObject().Equals(_actual);

        It should_not_be_equal = () => _result.ShouldBeFalse();
    }
}