using System;
using System.Diagnostics.Contracts;
using NHibernate;
using NHibernate.Context;

namespace NCommons.Persistence.NHibernate
{
    /// <summary>
    /// Delegates management of the active session to nhibernate's <see cref="CurrentSessionContext"/>.
    /// </summary>
    public class NHibernateSessionContextManager : IActiveSessionManager<ISession>
    {
        readonly ISessionFactory _sessionFactory;

        public NHibernateSessionContextManager(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        public bool HasActiveSession
        {
            get { return CurrentSessionContext.HasBind(_sessionFactory); }
        }

        public ISession GetActiveSession()
        {
            return _sessionFactory.GetCurrentSession();
        }

        public void SetActiveSession(ISession session)
        {
            Contract.Assert(session != null);
            CurrentSessionContext.Bind(session);
        }

        public void ClearActiveSession()
        {
            if (HasActiveSession)
            {
                CurrentSessionContext.Unbind(_sessionFactory);
            }
        }
    }
}