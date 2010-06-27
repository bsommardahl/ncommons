using System;
using Machine.Specifications;
using NCommons.Rules.Mapping;
using NCommons.Rules.Specs.TestModels;

namespace NCommons.Rules.Specs
{
    [Subject(typeof (AssociationConfiguration<TestMessage>))]
    public class when_configured_to_assocate_with_another_message
    {
        static Type _associatedType;
        static MockAssociationConfiguration _configuration;

        Establish context = () => { _configuration = new MockAssociationConfiguration(); };

        It should_return_associated_type =
            () => _configuration.GetDestinationMessageType().ShouldBeTheSameAs(typeof (TestEntity));

        It should_correctly_return_explicit_source_property_names =
            () => _configuration.GetSourcePropertyNameFor("EntityDescription").ShouldEqual("Description");
    }

    public class MockAssociationConfiguration : AssociationConfiguration<TestMessage>
    {
        public MockAssociationConfiguration()
        {
            ConfigureAssociationsFor<TestEntity>(c =>
                {
                    c.For(e => e.EntityId).Use(m => m.Description);
                    c.For(e => e.EntityDescription).Use(m => m.Description);
                });
        }
    }
}