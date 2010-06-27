#define SQLEXPRESS
using System;
using System.Reflection;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Machine.Specifications;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;
using NHibernate.Tool.hbm2ddl;

namespace NCommons.Persistence.NHibernate.Specs
{
    public abstract class given_an_nhibernate_context
    {
        protected static Configuration Configuration;
        protected static IDatabaseContext DatabaseContext;
        protected static ISessionFactory SessionFactory;

        Establish context = () =>
            {
                Console.WriteLine("Initializing NHibernate");
                InitializeNHibernate(Assembly.GetExecutingAssembly());
            };

        public static void InitializeNHibernate(params Assembly[] assemblies)
        {
            if (SessionFactory != null)
                return;

            Console.WriteLine("Creating a new SessionFactory");

#if SQLEXPRESS

            SessionFactory = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2000
                              .ConnectionString(
                              c =>
                              c.Is(
                                  @"Data Source=.\SQLEXPRESS;Initial Catalog=Test;Integrated Security=True;Pooling=False"))
                              .ShowSql())
                .Mappings(m => Array.ForEach(assemblies, a => m.FluentMappings.AddFromAssembly(a)))
                .ExposeConfiguration(configuration =>
                    {
                        configuration.SetProperty("current_session_context_class", "call");
                        Configuration = configuration;
                    })
                .BuildSessionFactory();
#endif

#if SQLITE

            SessionFactory = Fluently.Configure()
                .Database(SQLiteConfiguration.Standard
                              .InMemory()
                              .ShowSql())
                .Mappings(m => Array.ForEach(assemblies, a => m.FluentMappings.AddFromAssembly(a)))
                .ExposeConfiguration(configuration =>
                    {
                        configuration.SetProperty("current_session_context_class", "call");
                        Configuration = configuration;
                    })
                .BuildSessionFactory();

#endif


            DatabaseContext = new NHibernateDatabaseContext(new NHibernateSessionContextManager(SessionFactory), SessionFactory);

            var session = SessionFactory.OpenSession();
            CurrentSessionContext.Bind(session);

            // Export Schema
            new SchemaExport(Configuration).Execute( /* script to console is */ false,
                /* export to database is */ true,
                /* drop only is */ false, session.Connection,
                /* TextWriter is */ null);
        }
    }
}