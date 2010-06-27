using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NHibernate;
using NHibernate.Linq;

namespace NCommons.Persistence.NHibernate
{
    public class NHibernateRepository<T> : IRepository<T>
    {
        readonly ISessionFactory _sessionFactory;

        public NHibernateRepository(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        protected virtual ISession Session
        {
            get { return _sessionFactory.GetCurrentSession(); }
        }

        public T Get<TId>(TId id)
        {
            return Session.Get<T>(id);
        }

        public IEnumerable<T> Query(Expression<Func<T, bool>> specification)
        {
            return Query(new Specification<T>(specification));
        }

        public IEnumerable<T> Query(ISpecification<T> specification)
        {
            IQueryable<T> query = Session.Linq<T>();
            return query.Where(specification.Predicate);
        }

        public virtual IEnumerable<T> GetAll()
        {
            ICriteria criteria = _sessionFactory.GetCurrentSession().CreateCriteria(typeof (T));
            return criteria.List<T>();
        }

        public void Save(T entity)
        {
            Session.SaveOrUpdate(entity);
        }

        public void Delete(T entity)
        {
            Session.Delete(entity);
        }

        public IEnumerator<T> GetEnumerator()
        {
            ICriteria criteria = _sessionFactory.GetCurrentSession().CreateCriteria(typeof (T));
            return criteria.List<T>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }  
}