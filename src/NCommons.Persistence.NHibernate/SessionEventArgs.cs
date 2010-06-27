using System;
using NHibernate;

namespace NCommons.Persistence.NHibernate
{
    public class SessionEventArgs : EventArgs
    {
        public SessionEventArgs(ISession session)
        {
            Session = session;
        }

        public ISession Session { get; set; }
    }
}