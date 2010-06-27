using System;
using Machine.Specifications;
using Moq;
using NHibernate;
using It = Machine.Specifications.It;

namespace NCommons.Persistence.NHibernate.Specs
{
    namespace NHibernateDatabaseSessionSpecs
    {
        [Subject(typeof (NHibernateDatabaseSession))]
        public class when_flushing_changes
        {
            static NHibernateDatabaseSession _databaseSession;
            static Mock<ISession> _mockSession;

            Establish context = () =>
                {
                    _mockSession = new Mock<ISession>();
                    _databaseSession = new NHibernateDatabaseSession(_mockSession.Object);
                };

            Because of = () => _databaseSession.Flush();

            It should_flush_the_nhibernate_session = () => _mockSession.Verify(x => x.Flush());
        }

        [Subject(typeof (NHibernateDatabaseSession))]
        public class when_committing_changes
        {
            static NHibernateDatabaseSession _databaseSession;
            static Mock<ISession> _mockSession;
            static Mock<ITransaction> _mockTransaction;

            Establish context = () =>
                {
                    _mockSession = new Mock<ISession>();
                    _mockTransaction = new Mock<ITransaction>();
                    _mockSession.SetupGet(x => x.Transaction).Returns(_mockTransaction.Object);
                    _databaseSession = new NHibernateDatabaseSession(_mockSession.Object);
                };

            Because of = () => _databaseSession.Commit();

            It should_commit_the_nhibernate_transaction = () => _mockTransaction.Verify(x => x.Commit());
        }

        [Subject(typeof (NHibernateDatabaseSession))]
        public class when_rolling_back_changes
        {
            static NHibernateDatabaseSession _databaseSession;
            static Mock<ISession> _mockSession;
            static Mock<ITransaction> _mockTransaction;

            Establish context = () =>
                {
                    _mockSession = new Mock<ISession>();
                    _mockTransaction = new Mock<ITransaction>();
                    _mockSession.SetupGet(x => x.Transaction).Returns(_mockTransaction.Object);
                    _databaseSession = new NHibernateDatabaseSession(_mockSession.Object);
                };

            Because of = () => _databaseSession.Rollback();

            It should_rollback_the_nhibernate_transaction = () => _mockTransaction.Verify(x => x.Rollback());
        }

        [Subject(typeof (NHibernateDatabaseSession))]
        public class when_disposing
        {
            static NHibernateDatabaseSession _databaseSession;
            static Mock<ISession> _mockSession;

            Establish context = () =>
                {
                    _mockSession = new Mock<ISession>();
                    _databaseSession = new NHibernateDatabaseSession(_mockSession.Object);
                };

            Because of = () => _databaseSession.Dispose();

            It should_dispose_session_ = () => _mockSession.Verify(x => x.Dispose());
        }
    }

    [Subject(typeof (NHibernateDatabaseSession))]
    public class when_comparing_session_abstractions_encapsulating_the_same_session_for_equality
    {
        static bool _result;
        static NHibernateDatabaseSession _sessionA;
        static NHibernateDatabaseSession _sessionB;

        Establish context = () =>
            {
                var mockSession = new Mock<ISession>();
                _sessionA = new NHibernateDatabaseSession(mockSession.Object);
                _sessionB = new NHibernateDatabaseSession(mockSession.Object);
            };

        Because of = () => _result = _sessionA == _sessionB;

        It should_be_equal = () => _result.ShouldBeTrue();
    }

    [Subject(typeof (NHibernateDatabaseSession))]
    public class when_comparing_session_abstractions_encapsulating_the_same_session_for_inequality
    {
        static bool _result;
        static NHibernateDatabaseSession _sessionA;
        static NHibernateDatabaseSession _sessionB;

        Establish context = () =>
            {
                var mockSession = new Mock<ISession>();
                _sessionA = new NHibernateDatabaseSession(mockSession.Object);
                _sessionB = new NHibernateDatabaseSession(mockSession.Object);
            };

        Because of = () => _result = _sessionA != _sessionB;

        It should_be_equal = () => _result.ShouldBeFalse();
    }

    [Subject(typeof (NHibernateDatabaseSession))]
    public class when_comparing_session_abstractions_encapsulating_different_sessions_for_equality
    {
        static bool _result;
        static NHibernateDatabaseSession _sessionA;
        static NHibernateDatabaseSession _sessionB;

        Establish context = () =>
            {
                var mockSessionA = new Mock<ISession>();
                var mockSessionB = new Mock<ISession>();
                _sessionA = new NHibernateDatabaseSession(mockSessionA.Object);
                _sessionB = new NHibernateDatabaseSession(mockSessionB.Object);
            };

        Because of = () => _result = _sessionA == _sessionB;

        It should_not_be_equal = () => _result.ShouldBeFalse();
    }

    [Subject(typeof (NHibernateDatabaseSession))]
    public class when_comparing_session_abstractions_encapsulating_different_sessions_for_inequality
    {
        static bool _result;
        static NHibernateDatabaseSession _sessionA;
        static NHibernateDatabaseSession _sessionB;

        Establish context = () =>
            {
                var mockSessionA = new Mock<ISession>();
                var mockSessionB = new Mock<ISession>();
                _sessionA = new NHibernateDatabaseSession(mockSessionA.Object);
                _sessionB = new NHibernateDatabaseSession(mockSessionB.Object);
            };

        Because of = () => _result = _sessionA != _sessionB;

        It should_not_be_equal = () => _result.ShouldBeTrue();
    }

    [Subject(typeof (NHibernateDatabaseSession))]
    public class when_hashing_session
    {
        static int _hashCode;
        static Mock<ISession> _mockSession;
        static NHibernateDatabaseSession _session;

        Establish context = () =>
            {
                _mockSession = new Mock<ISession>();
                _session = new NHibernateDatabaseSession(_mockSession.Object);
            };

        Because of = () => _hashCode = _session.GetHashCode();

        It should_create_hash_from_encapsulated_session = () => _mockSession.Object.GetHashCode().ShouldEqual(_hashCode);
    }
}