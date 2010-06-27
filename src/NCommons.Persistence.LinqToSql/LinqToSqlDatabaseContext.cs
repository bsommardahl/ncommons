using System.Data.Linq;
using System.Diagnostics.Contracts;
using NCommons.Persistence.LinqToSql.Properties;

namespace NCommons.Persistence.LinqToSql
{
    public delegate IDataContext DataContextProvider();

    public class LinqToSqlDatabaseContext : IDatabaseContext
    {
        readonly IActiveSessionManager<IDataContext> _activeSessionManager;
        readonly DataContextProvider _dataContextProvider;

        public LinqToSqlDatabaseContext(IActiveSessionManager<IDataContext> activeSessionManager,
                                        DataContextProvider dataContextProvider)
        {
            Contract.Assert(activeSessionManager != null);
            Contract.Assert(dataContextProvider != null);

            _activeSessionManager = activeSessionManager;
            _dataContextProvider = dataContextProvider;
        }

        public IDatabaseSession OpenSession()
        {
            Contract.Assert(_dataContextProvider != null, Resources.DataContextProviderNotFound);
            Contract.Assert(_activeSessionManager != null);
            
            var dataContext = _dataContextProvider();
            _activeSessionManager.SetActiveSession(dataContext);
            return CreateDatabaseSession(dataContext);
        }

        protected virtual IDatabaseSession CreateDatabaseSession(IDataContext dataContext)
        {
            return new LinqToSqlDatabaseSession(dataContext);
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
    }
}