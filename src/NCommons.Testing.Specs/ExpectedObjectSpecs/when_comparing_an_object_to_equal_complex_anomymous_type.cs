using System.Collections.Generic;
using Machine.Specifications;
using NCommons.Testing.Equality;
using NCommons.Testing.Specs.TestTypes;

namespace NCommons.Testing.Specs.ExpectedObjectSpecs
{
    public class when_comparing_an_object_to_equal_complex_anomymous_type
    {
        static ComplexType _actual;
        static object _expected;

        static bool _result;

        Establish context = () =>
            {
                _expected = new
                                {
                                    StringProperty = "test string",
                                    DecimalProperty = 10.10m,
                                    IndexType = new IndexType<int>(new List<int> { 1, 2, 3, 4, 5 })
                                }.ToExpectedObject();

                _actual = new ComplexType
                              {
                                  StringProperty = "test string",
                                  DecimalProperty = 10.10m,
                                  IndexType = new IndexType<int>(new List<int> { 1, 2, 3, 4, 5 })
                              };
            };

        Because of = () => _result = _expected.ToExpectedObject().Equals(_actual);

        It should_be_equal = () => _result.ShouldBeTrue();
    }
}