using System;
using System.Data.Common;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace NCommons.Persistence.LinqToSql.Specs
{
    namespace LinqToSqlDatabaseContextSpecs
    {
        [Subject(typeof (LinqToSqlDatabaseContext))]
        public class when_creating_a_new_database_context_without_an_active_session_manager
        {
            static Exception _exception;

            Establish context = () => _exception = Catch.Exception(() => new LinqToSqlDatabaseContext(null, null));

            It should_fail = () => _exception.ShouldNotBeNull();
        }

        [Subject(typeof (LinqToSqlDatabaseContext))]
        public class when_opening_a_new_session : given_a_linq_database_context
        {
            static IDatabaseSession _session;

            Because of = () => _session = DatabaseContext.OpenSession();

            It should_return_a_session = () => _session.ShouldNotBeNull();
        }

        [Subject(typeof (LinqToSqlDatabaseContext))]
        public class when_getting_the_current_session_with_active_session : given_a_linq_database_context
        {
            static IDatabaseSession _session;

            Establish context = () => DatabaseContext.OpenSession();

            Because of = () => _session = DatabaseContext.GetCurrentSession();

            It should_return_the_active_session = () => _session.ShouldNotBeNull();
        }

        [Subject(typeof (LinqToSqlDatabaseContext))]
        public class when_commiting_a_session : given_a_linq_database_context
        {
            Establish adendum_context = () => DatabaseContext.OpenSession();

            Because of = () => DatabaseContext.CommitSession();

            It should_commit_the_open_session = () => MockTransaction.Verify(x => x.Commit());
        }

        [Subject(typeof(LinqToSqlDatabaseContext))]
        public class when_multiple_sessions_are_opened_within_differing_contexts : given_a_linq_database_context
        {
            static IDataContext context_A;
            static IDataContext context_B;
            static IDatabaseSession current_session_for_context_A;
            static LinqToSqlDatabaseContext databaseContext;
            static string the_current_context = string.Empty;
            static IDatabaseSession the_session_for_context_A;


            Establish context = () =>
            {
                // Setup conditional return based upon context
                MockActiveSessionManager.Setup(x => x.GetActiveSession())
                    .Returns(() => (the_current_context == "context a") ? context_A : context_B);

                // Define context provider to assign the created DataContext based upon the context property
                DataContextProvider provider = () =>
                {
                    var dataContext = new DataClassesDataContext();

                    if (the_current_context == "context a")
                        context_A = dataContext;
                    else
                        context_B = dataContext;

                    return dataContext;
                };

                databaseContext = new LinqToSqlDatabaseContext(MockActiveSessionManager.Object, provider);

                the_current_context = "context a";
                the_session_for_context_A = databaseContext.OpenSession();
                the_current_context = "context b";
                databaseContext.OpenSession();
            };

            Because of = () =>
            {
                the_current_context = "context a";
                current_session_for_context_A = databaseContext.GetCurrentSession();
            };

            It should_manage_session_lifetime_for_each_context =
                () => current_session_for_context_A.ShouldEqual(the_session_for_context_A);
        }

        public abstract class given_a_linq_database_context
        {
            protected static StubbedLinqToSqlDatabaseContext DatabaseContext;
            protected static Mock<IActiveSessionManager<IDataContext>> MockActiveSessionManager;
            protected static Mock<IDataContext> MockDataContext;
            protected static Mock<IDatabaseSession> MockDatabaseSession;
            protected static Mock<DbTransaction> MockTransaction;

            Establish context = () =>
                {
                    MockDataContext = new Mock<IDataContext>();
                    MockTransaction = new Mock<DbTransaction>();
                    MockDataContext.SetupGet(x => x.Transaction).Returns(MockTransaction.Object);
                    MockActiveSessionManager = new Mock<IActiveSessionManager<IDataContext>>();
                    MockActiveSessionManager.SetupGet(x => x.HasActiveSession).Returns(true);
                    MockActiveSessionManager.Setup(x => x.GetActiveSession()).Returns(MockDataContext.Object);
                    DatabaseContext = new StubbedLinqToSqlDatabaseContext(MockActiveSessionManager.Object,
                                                                          () => new DataClassesDataContext());
                    MockDatabaseSession = new Mock<IDatabaseSession>();
                    // set the mock DatabaseSession to be returned by the factory method
                    DatabaseContext.DatabaseSession = MockDatabaseSession.Object;
                };
        }

        public class StubbedLinqToSqlDatabaseContext : LinqToSqlDatabaseContext
        {
            public StubbedLinqToSqlDatabaseContext(IActiveSessionManager<IDataContext> activeSessionManager,
                                                   DataContextProvider dataContextProvider)
                : base(activeSessionManager, dataContextProvider)
            {
            }

            public IDatabaseSession DatabaseSession { get; set; }

            protected override IDatabaseSession CreateDatabaseSession(IDataContext dataContext)
            {
                return DatabaseSession;
            }
        }
    }
}