using System.Collections.Generic;
using System.Linq;
using Machine.Specifications;
using Moq;
using NCommons.Testing.Equality;
using NCommons.Testing.Specs.TestTypes;
using It = Machine.Specifications.It;

namespace NCommons.Testing.Specs.ExpectedObjectSpecs
{
    public class when_comparing_unequal_object_with_string_configured_with_writer
    {
        static TypeWithString _actual;
        static TypeWithString _expected;
        static ExpectedObject _expectedObject;
        static Mock<IWriter> _mockWriter;
        static List<EqualityResult> _results = new List<EqualityResult>();

        Establish context = () =>
            {
                _mockWriter = new Mock<IWriter>();
                _mockWriter
                    .Setup(x => x.Write(Moq.It.IsAny<EqualityResult>()))
                    .Callback<EqualityResult>(result => _results.Add(result));

                _expected = new TypeWithString {StringProperty = "test"};
                _expectedObject = new ExpectedObject(_expected).Configure(ctx =>
                    {
                        ctx.AddStrategy<ComparableComparisonStrategy>();
                        ctx.AddStrategy<ClassComparisonStrategy>();
                        ctx.WithWriter(_mockWriter.Object);
                    });

                _actual = new TypeWithString {StringProperty = "error"};
            };

        Because of = () => _expectedObject.Equals(_actual);

        It should_write_errors_to_the_writer =
            () => _mockWriter.Verify(x => x.Write(Moq.It.IsAny<EqualityResult>()), Times.AtLeastOnce());

        It should_write_string_compare_expected_result_to_the_writer =
            () => _results.Select(x => x.Member.Equals("StringProperty")).ShouldNotBeNull();
    }
}