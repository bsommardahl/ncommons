using System;
using Machine.Specifications;

namespace NCommons.Specifications.Specs
{
    public class when_should_equals_is_called_on_objects_with_unequal_strings
    {
        static TypeWithString _actualObject;
        static Exception _exception;
        static ExpectedObject<TypeWithString> _expectedObject;
        static bool _result;

        Establish context = () =>
            {
                _expectedObject = new ExpectedObject<TypeWithString>()
                .Set(x => x.SomeString).To("something expected");

                _actualObject = new TypeWithString
                                    {
                                        SomeString = "something wrong",
                                    };
            };

        Because of = () => _exception = Catch.Exception(() => _expectedObject.MyShouldEqual(_actualObject));

        It should_throw_exception = () => _exception.ShouldNotBeNull();

        It should_throw_exception_with_message_about_fault_property = () => _exception.Message.Contains("SomeString");
    }
}