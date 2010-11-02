using System;
using Machine.Specifications;
using NCommons.Testing.Equality;
using NCommons.Testing.Specs.TestTypes;

namespace NCommons.Testing.Specs.ExpectedObjectExtensionsSpecs
{
    public class when_equating_expected_object_to_unequal_actual_object
    {
        static ComplexType _actual;
        static Exception _exception;
        static ExpectedObject _expected;

        Establish context = () =>
            {
                _expected = new ComplexType
                                {
                                    StringProperty = "test string",
                                    TypeWithString = new TypeWithString
                                                         {
                                                             StringProperty = "inner test string"
                                                         }
                                }.ToExpectedObject();

                _actual = new ComplexType
                              {
                                  StringProperty = "test string1",
                                  TypeWithString = new TypeWithString
                                                       {
                                                           StringProperty = "inner test string2"
                                                       }
                              };
            };

        Because of = () => _exception = Catch.Exception(() => _expected.ShouldEqual(_actual));

        It should_throw_exception_with_error_for_StringProperty =
            () =>
            _exception.Message.ShouldContain("For StringProperty, expected \"test string\" but found \"test string1\".");

        It should_throw_exception_with_error_for_TypeWithString_StringProperty =
            () =>
            _exception.Message.ShouldContain("For TypeWithString.StringProperty, expected \"inner test string\" but found \"inner test string2\".");
    }
}