using System;
using System.Data;
using System.Data.Common;

namespace NCommons.Persistence.LinqToSql
{
    public class LinqToSqlDatabaseSession : IDatabaseSession
    {
        readonly IDataContext _dataContext;
        readonly DbTransaction _transaction;
        bool _disposed;

        public LinqToSqlDatabaseSession(IDataContext dataContext)
        {
            _dataContext = dataContext;
            if (_dataContext.Connection.State != ConnectionState.Open)
            {
                _dataContext.Connection.Open();
                _transaction = _dataContext.Connection.BeginTransaction();
            }

            _dataContext.Transaction = _transaction;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Flush()
        {
            _dataContext.SubmitChanges();
        }

        public void Commit()
        {
            _dataContext.SubmitChanges();
            _transaction.Commit();
        }

        public void Rollback()
        {
            _transaction.Rollback();
        }

        void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!_disposed)
                {
                    _transaction.Dispose();
                    _disposed = true;
                }
            }
        }

        public bool Equals(LinqToSqlDatabaseSession other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other._dataContext, _dataContext);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (LinqToSqlDatabaseSession)) return false;
            return Equals((LinqToSqlDatabaseSession) obj);
        }

        public override int GetHashCode()
        {
            return (_dataContext != null ? _dataContext.GetHashCode() : 0);
        }

        public static bool operator ==(LinqToSqlDatabaseSession left, LinqToSqlDatabaseSession right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(LinqToSqlDatabaseSession left, LinqToSqlDatabaseSession right)
        {
            return !Equals(left, right);
        }
    }
}