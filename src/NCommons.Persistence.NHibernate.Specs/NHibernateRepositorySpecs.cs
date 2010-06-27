using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Machine.Specifications;
using NCommons.Persistence.NHibernate.Specs.TestEntities;

namespace NCommons.Persistence.NHibernate.Specs
{
    namespace RepositorySpecs
    {
        [Subject(typeof (NHibernateRepository<TestEntity>))]
        public class when_getting_item_by_id : given_a_repository_of_type<TestEntity>
        {
            static TestEntity _results;

            Establish context = () => MockSession.Setup(x => x.Get<TestEntity>(1)).Returns(new TestEntity {Id = 1});

            Because of = () => _results = Repository.Get(1);

            It should_return_the_expected_item = () => _results.Id.ShouldEqual(1);
        }

        [Subject(typeof (NHibernateRepository<TestEntity>))]
        public class when_getting_all_items : given_a_repository_of_type<TestEntity>
        {
            static IEnumerable<TestEntity> _results;

            Establish amended_context =
                () => MockCriteria
                          .Setup(x => x.List<TestEntity>())
                          .Returns(new List<TestEntity>
                                       {
                                           new TestEntity {Id = 1},
                                           new TestEntity {Id = 2}
                                       });

            Because of = () => _results = Repository.GetAll();

            It should_return_all_items = () => _results.Count().ShouldEqual(2);
        }

        [Subject(typeof (NHibernateRepository<TestEntity>))]
        public class when_enumerating_all_items : given_a_repository_of_type<TestEntity>
        {
            static IEnumerable<TestEntity> _results;

            Establish amended_context =
                () => MockCriteria
                          .Setup(x => x.List<TestEntity>())
                          .Returns(new List<TestEntity>
                                       {
                                           new TestEntity {Id = 1},
                                           new TestEntity {Id = 2}
                                       });

            Because of = () => _results = Repository.Select(e => e);

            It should_return_all_items = () => _results.Count().ShouldEqual(2);
        }

        [Subject(typeof (NHibernateRepository<TestEntity>))]
        public class when_enumerating_all_items_as_enumerable : given_a_repository_of_type<TestEntity>
        {
            static IList<TestEntity> _results;

            Establish amended_context = () =>
                                        MockCriteria
                                            .Setup(x => x.List<TestEntity>())
                                            .Returns(new List<TestEntity>
                                                         {
                                                             new TestEntity
                                                                 {Id = 1},
                                                             new TestEntity
                                                                 {Id = 2}
                                                         });


            Because of = () =>
                {
                    _results = new List<TestEntity>();
                    var itemEnumerator = Repository as IEnumerable;

                    foreach (object item in itemEnumerator)
                    {
                        _results.Add((TestEntity) item);
                    }
                };


            It should_return_all_items = () => _results.Count().ShouldEqual(2);
        }

        [Subject(typeof (NHibernateRepository<TestEntity>))]
        public class when_saving_an_entity : given_a_repository_of_type<TestEntity>
        {
            static TestEntity _entity;

            Establish context = () => { _entity = new TestEntity {Description = "This is a test"}; };

            Because of = () => Repository.Save(_entity);

            It should_save_to_the_session = () => MockSession.Verify(x => x.SaveOrUpdate(_entity));
        }

        [Subject(typeof (NHibernateRepository<TestEntity>))]
        public class when_deleting_an_entity : given_a_repository_of_type<TestEntity>
        {
            static TestEntity _entity;

            Establish context = () => { _entity = new TestEntity {Description = "This is a test"}; };

            Because of = () => Repository.Delete(_entity);

            It should_delete_from_the_session = () => MockSession.Verify(x => x.Delete(_entity));
        }
    }
}