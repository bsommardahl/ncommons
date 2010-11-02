using System.Collections.Generic;
using Machine.Specifications;
using NCommons.Testing.Equality;
using NCommons.Testing.Specs.TestTypes;

namespace NCommons.Testing.Specs.ExpectedObjectSpecs
{
    public class when_comparing_equal_objects_with_two_indexes
    {
        static MultiIndexType _actual;
        static MultiIndexType _expected;

        static bool _result;

        Establish context = () =>
            {
                _actual = new MultiIndexType
                              {
                                  IndexType1 = new IndexType<int>(new List<int> { 1, 2, 3, 4, 5 }),
                                  IndexType2 = new IndexType<int>(new List<int> { 1, 2, 3, 4, 6 })

                              };

                _expected = new MultiIndexType
                                {
                                    IndexType1 = new IndexType<int>(new List<int> { 1, 2, 3, 4, 5 }),
                                    IndexType2 = new IndexType<int>(new List<int> { 1, 2, 3, 4, 6 })
                                };
            };

        Because of = () => _result = _expected.ToExpectedObject().Equals(_actual);

        It should_not_be_equal = () => _result.ShouldBeTrue();
    }
}