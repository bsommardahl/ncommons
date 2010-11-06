using System;
using System.Collections.Generic;
using Machine.Specifications;
using NCommons.Testing.Equality;
using NCommons.Testing.Specs.TestTypes;

namespace NCommons.Testing.Specs.ShouldExtensions
{
    public class when_comparing_should_equal_for_unequal_objects_with_ienumerable
    {
        static TypeWithIEnumerable _actual;
        static Exception _exception;
        static TypeWithIEnumerable _expected;

        Establish context = () =>
            {
                _expected = new TypeWithIEnumerable {Objects = new List<string> {"test1", "test2"}};
                _actual = new TypeWithIEnumerable {Objects = new List<string> {"test3", "test4"}};
            };

        Because of = () => _exception = Catch.Exception(() => _expected.ToExpectedObject().ShouldEqual(_actual));

        It should_throw_exception_with_subscripted_values =
            () =>
            _exception.Message.ShouldEqual(
                string.Format("For TypeWithIEnumerable.Objects[0], expected \"test1\" but found \"test3\".{0}" +
                              "For TypeWithIEnumerable.Objects[1], expected \"test2\" but found \"test4\".{0}", Environment.NewLine));
    }
}