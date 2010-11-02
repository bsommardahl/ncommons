using Machine.Specifications;
using NCommons.Testing.Specs.TestTypes;

namespace NCommons.Testing.Specs.AsSpecs
{
    public class when_fluently_casting_to_a_type
    {
        static bool _hasProperty;
        static object _instance;

        Establish context = () => { _instance = new TypeWithString(); };

        Because of = () => _hasProperty = _instance.As<TypeWithString>().HasProperty("StringProperty");

        It should_cast_to_the_specified_type = () => _hasProperty.ShouldBeTrue();
    }

    public static class ObjectExtensions
    {
        public static bool HasProperty<T>(this T instance, string propertyName)
        {
            return typeof (T).GetProperty(propertyName) != null;
        }
    }
}