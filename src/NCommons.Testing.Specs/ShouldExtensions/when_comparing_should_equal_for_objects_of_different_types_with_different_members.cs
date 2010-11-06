using System;
using Machine.Specifications;
using NCommons.Testing.Equality;
using NCommons.Testing.Specs.TestTypes;

namespace NCommons.Testing.Specs.ShouldExtensions
{
    public class when_comparing_should_equal_for_objects_of_different_types_with_different_members
    {
        static Exception _exception;
        static TypeWithDecimal _expected;
        static TypeWithString _actual;

        Establish context = () =>
            {
                _expected = new TypeWithDecimal { DecimalProperty = 10.0m };

                _actual = new TypeWithString { StringProperty = "this is a test" };
            };

        Because of = () => _exception = Catch.Exception(() => _expected.ToExpectedObject().IgnoreTypes().ShouldEqual(_actual));

        It should_thow_exception_with_missing_member_message = () => _exception.Message.ShouldEqual(string.Format(
          "For TypeWithString.DecimalProperty, expected [10.0] but member was missing.{0}", Environment.NewLine));
    }
}