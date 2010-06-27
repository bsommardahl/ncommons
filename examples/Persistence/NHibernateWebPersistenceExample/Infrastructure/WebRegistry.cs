using System.Web.Mvc;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NCommons.Persistence;
using NCommons.Persistence.NHibernate;
using NHibernate;
using StructureMap.Configuration.DSL;

namespace NHibernateWebPersistenceExample.Infrastructure
{
    public class WebRegistry : Registry
    {
        public WebRegistry()
        {
            // Convention-based registration for controllers
            Scan(x =>
                {
                    x.TheCallingAssembly();
                    x.AddAllTypesOf<IController>()
                        .NameBy(type => type.Name.Substring(0, type.Name.Length - 10));
                    x.WithDefaultConventions();
                });

            For<IControllerFactory>().Use<StructureMapControllerFactory>();
            For(typeof (IRepository<>)).Use(typeof (NHibernateRepository<>));

            ISessionFactory sessionFactory = GetSessionFactory();
            IActiveSessionManager<ISession> activeSessionManager = new NHibernateSessionContextManager(sessionFactory);
            IDatabaseContext databaseContext = new NHibernateDatabaseContext(activeSessionManager, sessionFactory);

            For<IActiveSessionManager<ISession>>().Use(activeSessionManager);
            For<ISessionFactory>().Use(sessionFactory);
            For<IDatabaseContext>().Use(databaseContext);
        }

        ISessionFactory GetSessionFactory()
        {
            var autoPersistenceModel = new AutoPersistenceModel();

            autoPersistenceModel
                .AddEntityAssembly(GetType().Assembly)
                .Where(t => t.Namespace.StartsWith("NHibernateWebPersistenceExample.Models"));

            ISessionFactory sessionFactory = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008
                              .ConnectionString(c =>
                                                c.Is(
                                                    @"Data Source=.\SQLEXPRESS;Initial Catalog=Example;Integrated Security=True;Pooling=False"))
                              .ShowSql())
                .Mappings(m => m.AutoMappings.Add(autoPersistenceModel))
                .ExposeConfiguration(innerConfiguration =>
                                     innerConfiguration.SetProperty("current_session_context_class", "web"))
                .BuildSessionFactory();

            return sessionFactory;
        }
    }

    public static class ConfigurationExtensions
    {
    }
}