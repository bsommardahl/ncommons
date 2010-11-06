using System;
using Machine.Specifications;
using NCommons.Testing.Equality;
using NCommons.Testing.Specs.TestTypes;

namespace NCommons.Testing.Specs.ShouldExtensions
{
    public class when_comparing_should_equal_for_object_and_null
    {
        static Exception _exception;
        static TypeWithString _expected;

        Establish context = () =>
            {
                _expected = new TypeWithString
                                {
                                    StringProperty = "test"
                                };
            };

        Because of = () => _exception = Catch.Exception(() => _expected.ToExpectedObject().ShouldEqual((object) null));

        It should_thow_exception_with_null_message = () => _exception.Message.ShouldEqual(string.Format(
            "For [null], expected NCommons.Testing.Specs.TestTypes.TypeWithString but found [null].{0}", Environment.NewLine));
    }
}