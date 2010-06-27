using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using log4net;
using log4net.Config;
using Microsoft.Practices.ServiceLocation;
using NCommons.Persistence;
using StructureMap;

namespace NHibernateWebPersistenceExample.Infrastructure
{
    public class Bootstrapper
    {
        Container _container;

        public void Run()
        {
            ConfigureLogging();
            ConfigureContainer();
            ConfigureAreas();
            ConfigureRoutes();
            ConfigureStaticFactories();
        }

        void ConfigureLogging()
        {
            ILog log = LogManager.GetLogger("Default");
            XmlConfigurator.Configure(new FileInfo(HttpContext.Current.Server.MapPath("~/log4net.xml")));
            log.Error("Logging configured.");
        }

        void ConfigureStaticFactories()
        {
            ServiceLocator.SetLocatorProvider(() => _container.GetInstance<StructureMapServiceLocator>());
            ControllerBuilder.Current.SetControllerFactory(_container.GetInstance<IControllerFactory>());
            DatabaseContextFactory.SetInstanceFactory(() => _container.GetInstance<IDatabaseContext>());
        }

        void ConfigureContainer()
        {
            _container = new Container();
            _container.Configure(x => { x.AddRegistry<WebRegistry>(); });
        }

        void ConfigureRoutes()
        {
            RouteTable.Routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            RouteTable.Routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new {controller = "Product", action = "Index", id = UrlParameter.Optional} // Parameter defaults
                );
        }

        void ConfigureAreas()
        {
            AreaRegistration.RegisterAllAreas();
        }
    }
}