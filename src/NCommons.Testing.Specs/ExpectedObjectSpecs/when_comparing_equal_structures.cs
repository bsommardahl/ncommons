using Machine.Specifications;
using NCommons.Testing.Equality;
using NCommons.Testing.Specs.TestTypes;

namespace NCommons.Testing.Specs.ExpectedObjectSpecs
{
    public class when_comparing_equal_structures
    {
        static StructureType _actual;
        static StructureType _expected;
        static bool _result;

        Establish context = () =>
            {
                _expected = new StructureType { IntegerValue = 2 };
                _actual = new StructureType { IntegerValue = 2 };
            };

        Because of = () => _result = _expected.ToExpectedObject().Equals(_actual);

        It should_be_equal = () => _result.ShouldBeTrue();
    }
}