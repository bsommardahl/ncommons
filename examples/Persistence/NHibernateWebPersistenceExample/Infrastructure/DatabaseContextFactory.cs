using System;
using NCommons.Persistence;

namespace NHibernateWebPersistenceExample.Infrastructure
{
    public static class DatabaseContextFactory
    {
        static Func<IDatabaseContext> _instanceFactory;

        public static void SetInstanceFactory(Func<IDatabaseContext> instanceFactory)
        {
            _instanceFactory = instanceFactory;
        }

        public static IDatabaseContext GetDatabaseContext()
        {
            return _instanceFactory();
        }
    }
}