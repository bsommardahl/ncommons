using System;
using Machine.Specifications;
using NCommons.Testing.Equality;
using NCommons.Testing.Specs.TestTypes;

namespace NCommons.Testing.Specs.ShouldExtensions
{
    public class when_comparing_should_equal_for_objects_of_different_types
    {
        static Exception _exception;
        static TypeWithString _expected;
        static TypeWithString2 _actual;

        Establish context = () =>
            {
                _expected = new TypeWithString()
                                {
                                    StringProperty = "this is a test"
                                };

                _actual = new TypeWithString2
                              {
                                  StringProperty = "this is a test"
                              };

                _expectedObject = new ExpectedObject(_expected);
            };

        Because of = () => _exception = Catch.Exception(() => _expectedObject.ShouldEqual(_actual));

        It should_throw_exception_with_TypeWithString_message = () => _exception.Message.ShouldEqual(
            string.Format(
                "For TypeWithString2, expected NCommons.Testing.Specs.TestTypes.TypeWithString but found NCommons.Testing.Specs.TestTypes.TypeWithString2.{0}",
                Environment.NewLine));

        static ExpectedObject _expectedObject;
    }
}