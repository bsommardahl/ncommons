using Machine.Specifications;
using Moq;
using NHibernate;

namespace NCommons.Persistence.NHibernate.Specs
{
    public abstract class given_a_repository_of_type<T>
    {
        protected static Mock<ICriteria> MockCriteria;
        protected static Mock<ISession> MockSession;
        protected static Mock<ISessionFactory> MockSessionFactory;
        protected static NHibernateRepository<T> Repository;

        Establish context = () =>
            {
                MockSessionFactory = new Mock<ISessionFactory>();
                MockSession = new Mock<ISession>();
                MockCriteria = new Mock<ICriteria>();

                MockSession.Setup(x => x.CreateCriteria(typeof (T))).Returns(MockCriteria.Object);
                MockSessionFactory.Setup(x => x.GetCurrentSession()).Returns(MockSession.Object);
                Repository = new NHibernateRepository<T>(MockSessionFactory.Object);
            };
    }
}