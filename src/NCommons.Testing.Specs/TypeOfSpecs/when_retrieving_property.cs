using Machine.Specifications;
using NCommons.Testing.Specs.TestTypes;

namespace NCommons.Testing.Specs.TypeOfSpecs
{
    public class when_retrieving_property
    {
        static string _name;

        Because of = () => _name = TypeOf<TypeWithString>.Property(t => t.StringProperty);

        It should_return_the_name_of_the_property = () => _name.ShouldEqual("StringProperty");
    }
}