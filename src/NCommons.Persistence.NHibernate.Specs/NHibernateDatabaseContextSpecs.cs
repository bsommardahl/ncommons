using Machine.Specifications;
using Moq;
using NHibernate;
using It = Machine.Specifications.It;

namespace NCommons.Persistence.NHibernate.Specs
{
    namespace NHibernateDatabaseContextSpecs
    {
        [Subject(typeof (NHibernateDatabaseContext))]
        public class when_opening_a_new_session : given_a_database_context
        {
            static IDatabaseSession _session;

            Because of = () => _session = DatabaseContext.OpenSession();

            It should_return_a_new_session = () => _session.ShouldNotBeNull();
        }

        [Subject(typeof (NHibernateDatabaseContext))]
        public class when_committing_the_session : given_a_database_context
        {
            Establish adendum_context = () =>
                {
                    MockActiveSessionManager
                        .Setup(x => x.GetActiveSession())
                        .Returns(MockSession.Object);
                    DatabaseContext.OpenSession();
                };

            Because of = () => DatabaseContext.CommitSession();

            It should_commit_the_transaction = () => MockTransaction.Verify(x => x.Commit());
        }

        [Subject(typeof (NHibernateDatabaseContext))]
        public class when_getting_the_current_session : given_a_database_context
        {
            static IDatabaseSession _session;
            static ISession _openSession;

            Establish adendum_context = () =>
                {
                    MockActiveSessionManager
                        .Setup(x => x.SetActiveSession(Moq.It.IsAny<ISession>()))
                        .Callback<ISession>(s => _openSession = s);

                    MockActiveSessionManager
                        .SetupGet(x => x.HasActiveSession).Returns(true);
                     

                    MockActiveSessionManager
                        .Setup(x => x.GetActiveSession())
                        .Returns(() => _openSession);

                    DatabaseContext.OpenSession();
                };

            Because of = () => _session = DatabaseContext.GetCurrentSession();

            It should_return_the_session = () => _session.ShouldNotBeNull();
        }

        [Subject(typeof(NHibernateDatabaseContext))]
        public class when_multiple_sessions_are_opened_within_differing_contexts : given_a_database_context
        {
            static ISession context_A;
            static ISession context_B;
            static IDatabaseSession current_session_for_context_A;
            static string the_current_context = string.Empty;
            static IDatabaseSession the_session_for_context_A;


            Establish context = () =>
            {
                context_A = new Mock<ISession>().Object;
                context_B = new Mock<ISession>().Object;

                MockSessionFactory.Setup(x => x.OpenSession())
                     .Returns(() => (the_current_context == "context a") ? context_A : context_B);

                MockActiveSessionManager.Setup(x => x.GetActiveSession())
                    .Returns(() => (the_current_context == "context a") ? context_A : context_B);

                the_current_context = "context a";
                the_session_for_context_A = DatabaseContext.OpenSession();
                the_current_context = "context b";
                DatabaseContext.OpenSession();
            };

            Because of = () =>
            {
                the_current_context = "context a";
                current_session_for_context_A = DatabaseContext.GetCurrentSession();
            };

            It should_manage_session_lifetime_for_each_context =
                () => current_session_for_context_A.ShouldEqual(the_session_for_context_A);
        }
    }
}