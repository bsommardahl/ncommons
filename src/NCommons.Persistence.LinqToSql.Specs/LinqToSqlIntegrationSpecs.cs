using System;
using System.Collections.Generic;
using System.Linq;
using Machine.Specifications;

namespace NCommons.Persistence.LinqToSql.Specs
{
    namespace RepositorySpecs
    {
        [Tags("integration")]
        [Subject(typeof (LinqToSqlDatabaseContext))]
        public class when_getting_an_entity_by_id
        {
            static LinqToSqlActiveSessionManager _activeSessionManager;
            static LinqToSqlDatabaseContext _databaseContext;
            static string _description;
            static Test _entity;
            static LinqToSqlRepository<Test> _repository;
            static IDatabaseSession _session;

            Cleanup after = () =>
                {
                    using (IDatabaseSession session = _databaseContext.OpenSession())
                    {
                        _activeSessionManager.GetActiveSession().GetTable<Test>().DeleteOnSubmit(_entity);
                        session.Commit();
                    }
                };

            Establish context = () =>
                {
                    _description = Guid.NewGuid().ToString();
                    _entity = new Test {Description = _description};

                    _activeSessionManager = new LinqToSqlActiveSessionManager();
                    _repository = new LinqToSqlRepository<Test>(_activeSessionManager);
                    _databaseContext = new LinqToSqlDatabaseContext(_activeSessionManager,
                                                                    () => new DataClassesDataContext());

                    using (IDatabaseSession session = _databaseContext.OpenSession())
                    {
                        _repository.Save(_entity);
                        session.Commit();
                    }
                };

            Because of = () =>
                {
                    using (IDatabaseSession session = _databaseContext.OpenSession())
                    {
                        _entity = _repository.Get(_entity.Id);
                    }
                };

            It should_retrieve_the_entity = () => _entity.Description.ShouldEqual(_description);
        }


        [Tags("integration")]
        [Subject("LinqToSql Persistence")]
        public class when_session_not_commited : given_a_linq_context
        {
            static IDatabaseSession _databaseSession;
            static Test _entity;
            static Exception _exception;
            static LinqToSqlRepository<Test> _repository;
            static Test _testEntity;

            Establish adendum_context = () =>
                {
                    _repository = new LinqToSqlRepository<Test>(ActiveSessionManager);
                    _testEntity = new Test {Description = "This is a test"};

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
        [Subject(typeof (LinqToSqlDatabaseContext))]
        public class when_saving_an_entity
        {
            static LinqToSqlActiveSessionManager _activeSessionManager;
            static LinqToSqlDatabaseContext _databaseContext;
            static string _description;
            static Test _entity;
            static LinqToSqlRepository<Test> _repository;

            Cleanup after = () =>
                {
                    using (IDatabaseSession session = _databaseContext.OpenSession())
                    {
                        _activeSessionManager.GetActiveSession().GetTable<Test>().DeleteOnSubmit(_entity);
                        session.Commit();
                    }
                };

            Establish context = () =>
                {
                    _description = Guid.NewGuid().ToString();
                    _entity = new Test {Description = _description};

                    _activeSessionManager = new LinqToSqlActiveSessionManager();
                    _repository = new LinqToSqlRepository<Test>(_activeSessionManager);
                    _databaseContext = new LinqToSqlDatabaseContext(_activeSessionManager,
                                                                    () => new DataClassesDataContext());

                    using (IDatabaseSession session = _databaseContext.OpenSession())
                    {
                        _repository.Save(_entity);
                        session.Commit();
                    }
                };

            Because of = () =>
                {
                    _entity = (from e in ((DataClassesDataContext) _activeSessionManager.GetActiveSession()).Tests
                               where e.Description.Equals(_description)
                               select e).FirstOrDefault();
                };

            It should_persist_the_entity = () => _entity.Description.ShouldEqual(_description);
        }

        [Tags("integration")]
        public class when_querying_by_linq
        {
            const int Id = 2;
            static LinqToSqlActiveSessionManager _activeSessionManager;
            static LinqToSqlDatabaseContext _databaseContext;
            static string _description;
            static Test _entity;
            static LinqToSqlRepository<Test> _repository;
            static Test _results;

            Cleanup after = () =>
                {
                    using (IDatabaseSession session = _databaseContext.OpenSession())
                    {
                        _activeSessionManager.GetActiveSession().GetTable<Test>().DeleteOnSubmit(_entity);
                        session.Commit();
                    }
                };

            Establish context = () =>
                {
                    _description = Guid.NewGuid().ToString();
                    _entity = new Test {Id = Id, Description = _description};

                    _activeSessionManager = new LinqToSqlActiveSessionManager();
                    _repository = new LinqToSqlRepository<Test>(_activeSessionManager);
                    _databaseContext = new LinqToSqlDatabaseContext(_activeSessionManager,
                                                                    () => new DataClassesDataContext());

                    using (IDatabaseSession session = _databaseContext.OpenSession())
                    {
                        _repository.Save(_entity);
                        session.Commit();
                    }
                };

            Because of = () => _results = _repository.Query(q => q.Where(x => x.Id == Id)).Single();

            It should_retrieve_the_expected_item = () => _results.Id.ShouldEqual(Id);
        }
    }
}