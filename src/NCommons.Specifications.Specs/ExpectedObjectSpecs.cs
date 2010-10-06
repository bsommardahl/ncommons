using System.Collections.Generic;
using System.Linq;
using Machine.Specifications;

namespace NCommons.Specifications.Specs
{
    public class when_comparing_equal_lists
    {
        static TypeWithList _actualObject;
        static ExpectedObject<TypeWithList> _expectedObject;
        static bool _result;

        Establish context = () =>
            {
                _expectedObject = new ExpectedObject<TypeWithList>()
                    .UsingComparison<IEnumerable<string>>((x, y) => x.SequenceEqual(y))
                    .Set(x => x.TheList).To(new TypeWithList
                                                {
                                                    TheList = new List<string> {"a", "b", "c"}
                                                });


                //.For(x => x.TheList).CompareWith((x, y) => x.SequenceEqual(y));

                _actualObject = new TypeWithList
                                    {
                                        TheList = new List<string> {"a", "b", "c"}
                                    };
            };

        Because of = () => _result = _expectedObject.Equals(_actualObject);

        It should_be_equal = () => _result.ShouldBeTrue();
    }

    public class when_comparing_objects_with_unequal_strings
    {
        static TypeWithString _actualObject;
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

        Because of = () => _result = _expectedObject.Equals(_actualObject);

        It should_be_not_be_equal = () => _result.ShouldBeFalse();
    }

    class TypeWithString
    {
        public string SomeString { get; set; }
    }

    class TypeWithList
    {
        public List<string> TheList { get; set; }
    }
}