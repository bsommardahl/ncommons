using NHibernate;

namespace NCommons.Persistence.NHibernate
{
    public class NHibernateDatabaseContext : IDatabaseContext
    {
        readonly IActiveSessionManager<ISession> _activeSessionManager;
        readonly ISessionFactory _sessionFactory;
        // NHibernateDatabaseSession _currentDatabaseSession;

        public NHibernateDatabaseContext(IActiveSessionManager<ISession> activeSessionManager,
                                         ISessionFactory sessionFactory)
        {
            _activeSessionManager = activeSessionManager;
            _sessionFactory = sessionFactory;
        }

        public IDatabaseSession OpenSession()
        {
            ISession session = _sessionFactory.OpenSession();
            _activeSessionManager.SetActiveSession(session);
            var databaseSession = new NHibernateDatabaseSession(session);
            databaseSession.Disposing += _currentDatabaseSession_Disposing;
            return databaseSession;
        }

        public void CommitSession()
        {
            _activeSessionManager.GetActiveSession().Transaction.Commit();
        }

        public IDatabaseSession GetCurrentSession()
        {
            IDatabaseSession session = null;
            if (_activeSessionManager.HasActiveSession)
            {
                session = new NHibernateDatabaseSession(_activeSessionManager.GetActiveSession());
            }

            return session;
        }

        void _currentDatabaseSession_Disposing(object sender, SessionEventArgs e)
        {
            _activeSessionManager.ClearActiveSession();
        }
    }
}