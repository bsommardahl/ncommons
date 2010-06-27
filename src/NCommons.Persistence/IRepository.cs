﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace NCommons.Persistence
{
    public interface IRepository<T> : IEnumerable<T>
    {
        T Get<TId>(TId id);
        IEnumerable<T> Query(ISpecification<T> specification);
        IEnumerable<T> Query(Expression<Func<T, bool>> specification);
        IEnumerable<T> GetAll();
        void Save(T entity);
        void Delete(T entity);
    }
}