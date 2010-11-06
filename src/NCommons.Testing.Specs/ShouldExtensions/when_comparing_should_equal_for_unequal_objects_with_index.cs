using System;
using System.Collections.Generic;
using Machine.Specifications;
using NCommons.Testing.Equality;
using NCommons.Testing.Specs.TestTypes;

namespace NCommons.Testing.Specs.ShouldExtensions
{
    public class when_comparing_should_equal_for_unequal_objects_with_index
    {
        static TypeWithIndexType _actual;
        static Exception _exception;
        static TypeWithIndexType _expected;

        Establish context = () =>
            {
                _expected = new TypeWithIndexType {Ints = new IndexType<int>(new List<int> {1, 2, 3, 4, 5})};
                _actual = new TypeWithIndexType {Ints = new IndexType<int>(new List<int> {1, 2, 3, 4, 6})};
            };

        Because of = () => _exception = Catch.Exception(() => _expected.ToExpectedObject().ShouldEqual(_actual));

        It should_throw_exception_with_subscripted_values =
            () =>
            _exception.Message.ShouldEqual(
                string.Format("For TypeWithIndexType.Ints.Item[4], expected [5] but found [6].{0}", Environment.NewLine));
    }
}