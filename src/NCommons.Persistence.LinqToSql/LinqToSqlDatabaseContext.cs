using System;

namespace NCommons.Persistence.LinqToSql
{
    public delegate IDataContext DataContextProvider();

    public class LinqToSqlDatabaseContext : IDatabaseContext
    {
        private readonly IActiveSessionManager<IDataContext> _activeSessionManager;
        private readonly DataContextProvider _dataContextProvider;

        public LinqToSqlDatabaseContext(IActiveSessionManager<IDataContext> activeSessionManager,
                                        DataContextProvider dataContextProvider)
        {
            if (activeSessionManager == null) throw new ArgumentNullException("activeSessionManager");
            if (dataContextProvider == null) throw new ArgumentNullException("dataContextProvider");

            _activeSessionManager = activeSessionManager;
            _dataContextProvider = dataContextProvider;
        }

        #region IDatabaseContext Members

        public IDatabaseSession OpenSession()
        {
            IDataContext dataContext = _dataContextProvider();
            _activeSessionManager.SetActiveSession(dataContext);
            return CreateDatabaseSession(dataContext);
        }

        public void CommitSession()
        {
            if (_activeSessionManager.HasActiveSession)
            {
                IDataContext session = _activeSessionManager.GetActiveSession();
                session.SubmitChanges();
                session.Transaction.Commit();
                session.Dispose();
            }
        }

        public IDatabaseSession GetCurrentSession()
        {
            IDatabaseSession databaseSession = null;

            if (_activeSessionManager.HasActiveSession)
                databaseSession = CreateDatabaseSession(_activeSessionManager.GetActiveSession());

            return databaseSession;
        }

        #endregion

        protected virtual IDatabaseSession CreateDatabaseSession(IDataContext dataContext)
        {
            return new LinqToSqlDatabaseSession(dataContext);
        }
    }
}