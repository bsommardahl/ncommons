using FluentNHibernate.Mapping;
using NCommons.Persistence.NHibernate.Specs.TestEntities;

namespace NCommons.Persistence.NHibernate.Specs.Mappings
{
    public class TestEntityMap : ClassMap<TestEntity>
    {
        public TestEntityMap()
        {
            Table("Test");
            Id(x => x.Id);
            Map(x => x.Description);
            Map(x => x.SomeField);
        }
    }
}