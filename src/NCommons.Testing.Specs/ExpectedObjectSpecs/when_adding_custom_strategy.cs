using System.Collections.Generic;
using Machine.Specifications;
using Moq;
using NCommons.Testing.Equality;
using NCommons.Testing.Specs.TestTypes;
using It = Machine.Specifications.It;

namespace NCommons.Testing.Specs.ExpectedObjectSpecs
{
    public class when_adding_a_strategy
    {
        static TypeWithIEnumerable _actual;
        static ExpectedObject _expected;

        static bool _result;
        static Mock<IComparisonStrategy> _comparisonStrategyMock;

        Establish context = () =>
            {
                _comparisonStrategyMock = new Mock<IComparisonStrategy>();

                var expected = new TypeWithIEnumerable {Objects = new List<string> {"test string"}};
                _expected = new ExpectedObject(expected)
                    .Configure(ctx => ctx.AddStrategy(_comparisonStrategyMock.Object));
                _actual = new TypeWithIEnumerable {Objects = new List<string> {"test string"}};
            };

        Because of = () => _result = _expected.Equals(_actual);

        It should_use_the_strategy = () => _result.ShouldBeTrue();
    }
}