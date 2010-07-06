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
            private static Exception _exception;

            private Establish context =
                () => _exception = Catch.Exception(() => new LinqToSqlDatabaseContext(null, null));

            private It should_fail = () => _exception.ShouldNotBeNull();
        }

        [Subject(typeof (LinqToSqlDatabaseContext))]
        public class when_opening_a_new_session : given_a_linq_database_context
        {
            private static IDatabaseSession _session;

            private Because of = () => _session = DatabaseContext.OpenSession();

            private It should_return_a_session = () => _session.ShouldNotBeNull();
        }

        [Subject(typeof (LinqToSqlDatabaseContext))]
        public class when_getting_the_current_session_with_active_session : given_a_linq_database_context
        {
            private static IDatabaseSession _session;

            private Establish context = () => DatabaseContext.OpenSession();

            private Because of = () => _session = DatabaseContext.GetCurrentSession();

            private It should_return_the_active_session = () => _session.ShouldNotBeNull();
        }

        [Subject(typeof (LinqToSqlDatabaseContext))]
        public class when_commiting_a_session : given_a_linq_database_context
        {
            private Establish adendum_context = () => DatabaseContext.OpenSession();

            private Because of = () => DatabaseContext.CommitSession();

            private It should_commit_the_open_session = () => MockTransaction.Verify(x => x.Commit());
        }

        [Subject(typeof (LinqToSqlDatabaseContext))]
        public class when_multiple_sessions_are_opened_within_differing_contexts : given_a_linq_database_context
        {
            private static IDataContext context_A;
            private static IDataContext context_B;
            private static IDatabaseSession current_session_for_context_A;
            private static LinqToSqlDatabaseContext databaseContext;
            private static string the_current_context = string.Empty;
            private static IDatabaseSession the_session_for_context_A;
            private static Mock<IDataContext> _mockDataContextA;
            private static Mock<IDataContext> _mockDataContextB;
            private static Mock<DbConnection> _mockConnection;

            private Establish context = () =>
                                            {
                                                // Setup conditional return based upon context
                                                MockActiveSessionManager.Setup(x => x.GetActiveSession())
                                                    .Returns(() =>
                                                             (the_current_context == "context a")
                                                                 ? context_A
                                                                 : context_B);

                                                _mockDataContextA = new Mock<IDataContext>();
                                                _mockConnection = new Mock<DbConnection>();
                                                _mockDataContextA.Setup(x => x.Connection).Returns(
                                                    _mockConnection.Object);
                                                context_A = _mockDataContextA.Object;

                                                _mockDataContextB = new Mock<IDataContext>();
                                                _mockDataContextB.Setup(x => x.Connection).Returns(
                                                    _mockConnection.Object);
                                                context_B = _mockDataContextB.Object;

                                                // Define context provider to assign the created DataContext based upon the context property
                                                DataContextProvider provider
                                                    = () =>
                                                          {
                                                              if (the_current_context == "context a")
                                                                  return context_A;
                                                              else
                                                                  return context_B;
                                                          };

                                                databaseContext =
                                                    new LinqToSqlDatabaseContext(MockActiveSessionManager.Object,
                                                                                 provider);

                                                the_current_context = "context a";
                                                the_session_for_context_A = databaseContext.OpenSession();
                                                the_current_context = "context b";
                                                databaseContext.OpenSession();
                                            };

            private Because of = () =>
                                     {
                                         // Set the context to 'a' to trigger the MockActiveSessionManager to return the expected context
                                         the_current_context = "context a";
                                         current_session_for_context_A = databaseContext.GetCurrentSession();
                                     };

            private It should_manage_session_lifetime_for_each_context =
                () => current_session_for_context_A.ShouldEqual(the_session_for_context_A);
        }

        public abstract class given_a_linq_database_context
        {
            protected static StubbedLinqToSqlDatabaseContext DatabaseContext;
            protected static Mock<IActiveSessionManager<IDataContext>> MockActiveSessionManager;
            protected static Mock<IDataContext> MockDataContext;
            protected static Mock<IDatabaseSession> MockDatabaseSession;
            protected static Mock<DbTransaction> MockTransaction;

            private Establish context = () =>
                                            {
                                                MockDataContext = new Mock<IDataContext>();
                                                MockTransaction = new Mock<DbTransaction>();
                                                MockDataContext.SetupGet(x => x.Transaction).Returns(
                                                    MockTransaction.Object);
                                                MockActiveSessionManager =
                                                    new Mock<IActiveSessionManager<IDataContext>>();
                                                MockActiveSessionManager.SetupGet(x => x.HasActiveSession).Returns(true);
                                                MockActiveSessionManager.Setup(x => x.GetActiveSession()).Returns(
                                                    MockDataContext.Object);
                                                DatabaseContext =
                                                    new StubbedLinqToSqlDatabaseContext(MockActiveSessionManager.Object,
                                                                                        () =>
                                                                                        new DataClassesDataContext());
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