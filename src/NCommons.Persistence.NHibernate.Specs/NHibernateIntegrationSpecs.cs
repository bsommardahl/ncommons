using System;
using System.Linq;
using Machine.Specifications;
using NCommons.Persistence.NHibernate.Specs.TestEntities;
using NHibernate.Context;

namespace NCommons.Persistence.NHibernate.Specs
{
    namespace NHibernateIntegrationSpecs
    {
        [Tags("integration")]
        [Subject("NHibernate Persistence")]
        public class when_getting_item_by_id : given_an_nhibernate_context
        {
            const int _id = 1;
            static IDatabaseSession _databaseSession;
            static TestEntity _entity;
            static NHibernateRepository<TestEntity> _repository;
            static TestEntity _testEntity;

            Establish adendum_context = () =>
                {
                    _repository = new NHibernateRepository<TestEntity>(SessionFactory);
                    _testEntity = new TestEntity {Description = "This is a test"};

                    // Use existing session for in memory database scenarios
                    _databaseSession = DatabaseContext.GetCurrentSession() ?? DatabaseContext.OpenSession();
                    _repository.Save(_testEntity);
                };

            Cleanup after = () =>
                {
                    SessionFactory.GetCurrentSession().Delete(_entity);
                    _databaseSession.Commit();
                    _databaseSession.Dispose();
                    CurrentSessionContext.Unbind(SessionFactory);
                };

            Because of = () => _entity = SessionFactory.GetCurrentSession().Get<TestEntity>(_id);

            It should_retrieve_the_expected_item = () => _entity.Id.ShouldEqual(_id);
        }

        [Tags("integration")]
        [Subject("NHibernate Persistence")]
        public class when_session_not_commited : given_an_nhibernate_context
        {
            static IDatabaseSession _databaseSession;
            static TestEntity _entity;
            static Exception _exception;
            static NHibernateRepository<TestEntity> _repository;
            static TestEntity _testEntity;

            Establish adendum_context = () =>
                {
                    _repository = new NHibernateRepository<TestEntity>(SessionFactory);
                    _testEntity = new TestEntity {Description = "This is a test"};

                    using (DatabaseContext.OpenSession())
                    {
                        _repository.Save(_testEntity);
                    }
                };

            Because of = () =>
                {
                    using (DatabaseContext.OpenSession())
                    {
                        _entity = _repository.Get(1);
                    }
                };

            It should_not_persist_work_to_the_database = () => _entity.ShouldBeNull();
        }

        [Tags("integration")]
        [Subject("NHibernate Persistence")]
        public class when_querying_by_specification : given_an_nhibernate_context
        {
            const int _id = 1;
            static IDatabaseSession _databaseSession;
            static TestEntity _entity;
            static NHibernateRepository<TestEntity> _repository;
            static TestEntity _results;
            static TestEntity _testEntity;

            Establish adendum_context = () =>
                {
                    _repository = new NHibernateRepository<TestEntity>(SessionFactory);
                    _testEntity = new TestEntity {Description = "This is a test", SomeField = 1};

                    // Use existing session for in memory database scenarios
                    _databaseSession = DatabaseContext.GetCurrentSession() ?? DatabaseContext.OpenSession();
                    _repository.Save(_testEntity);
                };

            Cleanup after = () =>
                {
                    SessionFactory.GetCurrentSession().Delete(_testEntity);
                    _databaseSession.Commit();
                    _databaseSession.Dispose();
                    CurrentSessionContext.Unbind(SessionFactory);
                };

            Because of = () =>
                {
                    var someFieldSpecification = new Specification<TestEntity>(x => x.SomeField == 1);
                    var descriptionSpecification =
                        new Specification<TestEntity>(x => x.Description.Equals("This is a test"));
                    _results = _repository.Query(someFieldSpecification.And(descriptionSpecification)).Single();
                };

            It should_retrieve_the_expected_item = () => _results.SomeField.ShouldEqual(1);
        }

        [Tags("integration")]
        [Subject("NHibernate Persistence")]
        public class when_querying_by_expression_specification : given_an_nhibernate_context
        {
            const int _id = 1;
            static IDatabaseSession _databaseSession;
            static TestEntity _entity;
            static NHibernateRepository<TestEntity> _repository;
            static TestEntity _results;
            static TestEntity _testEntity;

            Establish adendum_context = () =>
                {
                    _repository = new NHibernateRepository<TestEntity>(SessionFactory);
                    _testEntity = new TestEntity {Description = "This is a test", SomeField = 1};

                    // Use existing session for in memory database scenarios
                    _databaseSession = DatabaseContext.GetCurrentSession() ?? DatabaseContext.OpenSession();
                    _repository.Save(_testEntity);
                };

            Cleanup after = () =>
                {
                    SessionFactory.GetCurrentSession().Delete(_testEntity);
                    _databaseSession.Commit();
                    _databaseSession.Dispose();
                    CurrentSessionContext.Unbind(SessionFactory);
                };

            Because of = () => { _results = _repository.Query(x => x.SomeField == 1).Single(); };

            It should_retrieve_the_expected_item = () => _results.SomeField.ShouldEqual(1);
        }
    }
}