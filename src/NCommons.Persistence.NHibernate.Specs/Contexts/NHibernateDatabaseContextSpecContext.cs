using Machine.Specifications;
using Moq;
using NHibernate;
using NHibernate.Engine;

namespace NCommons.Persistence.NHibernate.Specs
{
    public abstract class given_a_database_context
    {
        protected static NHibernateDatabaseContext DatabaseContext;
        protected static Mock<IActiveSessionManager<ISession>> MockActiveSessionManager;
        protected static Mock<IDatabaseSession> MockDatabaseSession;
        protected static Mock<ISession> MockSession;
        protected static Mock<ISessionFactory> MockSessionFactory;
        protected static Mock<ISessionFactoryImplementor> MockSessionFactoryImplementor;
        protected static Mock<ITransaction> MockTransaction;


        Establish context = () =>
            {
                MockSessionFactory = new Mock<ISessionFactory>();
                MockSession = new Mock<ISession>();
                MockTransaction = new Mock<ITransaction>();
                MockDatabaseSession = new Mock<IDatabaseSession>();
                MockActiveSessionManager = new Mock<IActiveSessionManager<ISession>>();
                MockActiveSessionManager.SetupGet(x => x.HasActiveSession).Returns(true);

                MockSessionFactory.Setup(x => x.OpenSession())
                    .Returns(MockSession.Object);
                MockSessionFactory.Setup(x => x.GetCurrentSession())
                    .Returns(MockSession.Object);
                MockSessionFactoryImplementor = MockSessionFactory.As<ISessionFactoryImplementor>();

                // Current setup
                MockSession.SetupGet(x => x.Transaction)
                    .Returns(MockTransaction.Object);
                MockSession.Setup(x => x.BeginTransaction())
                    .Returns(MockTransaction.Object);
                MockSession.SetupGet(x => x.SessionFactory)
                    .Returns(MockSessionFactoryImplementor.Object);

                DatabaseContext = new NHibernateDatabaseContext(MockActiveSessionManager.Object,
                                                                MockSessionFactoryImplementor.Object);
            };
    }
}