using System.Data.Linq;
using System.Diagnostics.Contracts;

namespace NCommons.Persistence.LinqToSql
{
    public class LinqToSqlActiveSessionManager : IActiveSessionManager<IDataContext>
    {
        IDataContext _session;

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
            Contract.Assume(session != null);
            _session = session;
        }

        public void ClearActiveSession()
        {
            _session = null;
        }
    }
}