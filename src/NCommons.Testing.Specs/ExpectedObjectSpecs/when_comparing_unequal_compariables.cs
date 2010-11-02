using Machine.Specifications;
using NCommons.Testing.Equality;
using NCommons.Testing.Specs.TestTypes;

namespace NCommons.Testing.Specs.ExpectedObjectSpecs
{
    public class when_comparing_unequal_compariables
    {
        static ComparableType _actual;
        static ComparableType _expected;

        static bool _result;

        Establish context = () =>
            {
                _actual = new ComparableType(true);
                _expected = new ComparableType(false);
            };

        Because of = () => _result = _expected.ToExpectedObject().Equals(_actual);

        It should_not_be_equal = () => _result.ShouldBeFalse();
    }
}