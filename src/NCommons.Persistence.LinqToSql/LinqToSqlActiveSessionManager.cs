using System;

namespace NCommons.Persistence.LinqToSql
{
    public class LinqToSqlActiveSessionManager : IActiveSessionManager<IDataContext>
    {
        private IDataContext _session;

        #region IActiveSessionManager<IDataContext> Members

        public bool HasActiveSession
        {
            get { return _session == null; }
        }

        public IDataContext GetActiveSession()
        {
            return _session;
        }

        public void SetActiveSession(IDataContext session)
        {
            if (session == null) throw new ArgumentNullException("session");
            _session = session;
        }

        public void ClearActiveSession()
        {
            _session = null;
        }

        #endregion
    }
}