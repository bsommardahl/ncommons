using System;
using Microsoft.Practices.ServiceLocation;
using NHibernate;
using NHibernate.Context;
using NHibernate.Engine;

namespace NCommons.Persistence.NHibernate
{
    /// <summary>
    /// Provides an adapter from NHibernate's <see cref="ICurrentSessionContext"/> to <see cref="IActiveSessionManager{TSession}"/>.
    /// </summary>
    /// <remarks>
    /// This adapter enables implementations of <see cref="IActiveSessionManager{TSession}"/> to be registered
    /// with NHibernate as an <see cref="ICurrentSessionContext"/>.  Since this type is instantiated by NHibernate,
    /// the Common Service Locator is used to retrieve an instance of <see cref="IActiveSessionManager{TSession}"/>.
    /// To use, this type should be registered using NHibernate's configuration property: "current_session_context_class".
    /// </remarks>
    public class SessionContextManagerAdapter : CurrentSessionContext
    {
        readonly ISessionFactoryImplementor _factory;

        public SessionContextManagerAdapter(ISessionFactoryImplementor factory)
        {
            _factory = factory;
        }

        protected override ISession Session
        {
            get
            {
                var activeSessionManager = ServiceLocator.Current.GetInstance<IActiveSessionManager<ISession>>();
                return activeSessionManager.GetActiveSession();
            }
            set
            {
                var activeSessionManager = ServiceLocator.Current.GetInstance<IActiveSessionManager<ISession>>();
                activeSessionManager.SetActiveSession(value);
            }
        }
    }
}