using System;
using NHibernate;

namespace NCommons.Persistence.NHibernate
{
    public class NHibernateDatabaseSession : IDatabaseSession
    {
        protected readonly ISession Session;

        public NHibernateDatabaseSession(ISession session)
        {
            Session = session;
            Session.BeginTransaction();
        }

        public virtual void Flush()
        {
            Session.Flush();
        }

        public virtual void Commit()
        {
            Session.Transaction.Commit();
        }

        public virtual void Rollback()
        {
            Session.Transaction.Rollback();
        }

        public virtual void Dispose()
        {
            OnDisposing(new SessionEventArgs(Session));
            Session.Dispose();
        }

        public event EventHandler<SessionEventArgs> Disposing;
        
        public void OnDisposing(SessionEventArgs args)
        {
            EventHandler<SessionEventArgs> handler = Disposing;

            if (handler != null)
                handler(this, args);
        }

        public bool Equals(NHibernateDatabaseSession other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.Session, Session);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (NHibernateDatabaseSession)) return false;
            return Equals((NHibernateDatabaseSession) obj);
        }

        public override int GetHashCode()
        {
            return (Session != null ? Session.GetHashCode() : 0);
        }

        public static bool operator ==(NHibernateDatabaseSession left, NHibernateDatabaseSession right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(NHibernateDatabaseSession left, NHibernateDatabaseSession right)
        {
            return !Equals(left, right);
        }
    }
}