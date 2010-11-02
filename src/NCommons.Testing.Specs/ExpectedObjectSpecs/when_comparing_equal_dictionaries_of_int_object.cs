﻿using System.Collections.Generic;
using Machine.Specifications;
using NCommons.Testing.Equality;

namespace NCommons.Testing.Specs.ExpectedObjectSpecs
{
    public class when_comparing_equal_dictionaries_of_int_object
    {
        static IDictionary<int, object> _actual;
        static IDictionary<int, object> _expected;

        static bool _result;

        Establish context = () =>
            {
                var someObject = new object();
                _actual = new Dictionary<int, object> { { 1, someObject  } };
                _expected = new Dictionary<int, object> { { 1, someObject } };
            };

        Because of = () => _result = _expected.ToExpectedObject().Equals(_actual);

        It should_be_equal = () => _result.ShouldBeTrue();
    }
}