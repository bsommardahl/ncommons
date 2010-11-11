using System.Collections.Generic;
using Machine.Specifications;
using NCommons.Testing.Equality;
using NCommons.Testing.Specs.TestTypes;

namespace NCommons.Testing.Specs.ExpectedObjectSpecs
{
    public class when_comparing_equal_objects_with_an_index
    {
        static IndexType<int> _actual;
        static IndexType<int> _expected;

        static bool _result;

        Establish context = () =>
            {
                _expected = new IndexType<int>(new List<int> {1, 2, 3, 4, 5});
                _actual = new IndexType<int>(new List<int> {1, 2, 3, 4, 5});
            };

        Because of = () => _result = _expected.ToExpectedObject().Equals(_actual);

        It should_be_equal = () => _result.ShouldBeTrue();
    }
}