using System;
using Machine.Specifications;
using NCommons.Testing.Equality;
using NCommons.Testing.Specs.TestTypes;

namespace NCommons.Testing.Specs.ExpectedObjectSpecs
{
    public class when_comparing_equal_compariables
    {
        static ComparableType _actual;
        static ComparableType _expected;

        static bool _result;

        Establish context = () =>
            {
                _actual = new ComparableType(true);
                _expected = new ComparableType(true);
            };

        Because of = () => _result = _expected.ToExpectedObject().Equals(_actual);

        It should_be_equal = () => _result.ShouldBeTrue();
    }
}