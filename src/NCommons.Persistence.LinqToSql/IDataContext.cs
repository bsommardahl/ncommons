using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.IO;
using System.Linq;

namespace NCommons.Persistence.LinqToSql
{
    public interface IDataContext : IDisposable
    {
        DbConnection Connection { get; }
        DbTransaction Transaction { get; set; }
        int CommandTimeout { get; set; }
        TextWriter Log { get; set; }
        bool ObjectTrackingEnabled { get; set; }
        bool DeferredLoadingEnabled { get; set; }
        MetaModel Mapping { get; }
        DataLoadOptions LoadOptions { get; set; }
        ChangeConflictCollection ChangeConflicts { get; }
        Table<TEntity> GetTable<TEntity>() where TEntity : class;
        ITable GetTable(Type type);
        bool DatabaseExists();
        void CreateDatabase();
        void DeleteDatabase();
        void SubmitChanges();
        void SubmitChanges(ConflictMode failureMode);
        void Refresh(RefreshMode mode, object entity);
        void Refresh(RefreshMode mode, params object[] entities);
        void Refresh(RefreshMode mode, IEnumerable entities);
        DbCommand GetCommand(IQueryable query);
        ChangeSet GetChangeSet();
        int ExecuteCommand(string command, params object[] parameters);
        IEnumerable<TResult> ExecuteQuery<TResult>(string query, params object[] parameters);
        IEnumerable ExecuteQuery(Type elementType, string query, params object[] parameters);
        IEnumerable<TResult> Translate<TResult>(DbDataReader reader);
        IEnumerable Translate(Type elementType, DbDataReader reader);
        IMultipleResults Translate(DbDataReader reader);
    }
}