using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Linq.Expressions;

namespace NCommons.Persistence.LinqToSql
{
    public class LinqToSqlRepository<T> : IRepository<T> where T : class
    {
        readonly IActiveSessionManager<IDataContext> _activeSessionManager;
        readonly DataContext _dataContext;

        public LinqToSqlRepository(IActiveSessionManager<IDataContext> activeSessionManager)
        {
            _activeSessionManager = activeSessionManager;
        }

        public T Get<TId>(TId id)
        {
            ParameterExpression itemParameter = Expression.Parameter(typeof (T), "item");
            Expression<Func<T, bool>> whereExpression = Expression.Lambda<Func<T, bool>>
                (
                    Expression.Equal(
                        Expression.Property(itemParameter, "Id"),
                        Expression.Constant(id)), new[] {itemParameter}
                );

            return GetAll().Where(whereExpression.Compile()).SingleOrDefault();
        }

        public IEnumerable<T> Query(Expression<Func<T, bool>> specification)
        {
            return Query(new Specification<T>(specification));
        }

        public IEnumerable<T> Query(ISpecification<T> specification)
        {
            return _dataContext.GetTable<T>().Where(specification.Predicate);
        }

        public IEnumerable<T> GetAll()
        {
            return _activeSessionManager.GetActiveSession().GetTable<T>();
        }

        public void Save(T entity)
        {
            _activeSessionManager.GetActiveSession().GetTable<T>().InsertOnSubmit(entity);
        }

        public void Delete(T entity)
        {
            _activeSessionManager.GetActiveSession().GetTable<T>().DeleteOnSubmit(entity);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _activeSessionManager.GetActiveSession().GetTable<T>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}