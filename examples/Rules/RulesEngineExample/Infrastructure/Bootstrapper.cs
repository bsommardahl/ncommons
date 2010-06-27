using System.Web.Mvc;
using AutoMapper;
using Microsoft.Practices.ServiceLocation;
using RulesEngineExample.Domain;
using RulesEngineExample.Models;
using StructureMap;

namespace RulesEngineExample.Infrastructure
{
    public class Bootstrapper
    {
        IContainer _container;

        public void Run()
        {
            ConfigureContainer();
            ConfigureServiceLocator();
            ConfigureControllerFactory();
            ConfigureMappings();
        }

        static void ConfigureMappings()
        {
            Mapper.CreateMap(typeof (ProductInput), typeof (Product));
        }

        void ConfigureContainer()
        {
            _container = new Container(c =>
                                       c.Scan(x =>
                                           {
                                               x.TheCallingAssembly();
                                               x.LookForRegistries();
                                           }));
        }

        void ConfigureServiceLocator()
        {
            ServiceLocator.SetLocatorProvider(_container.GetInstance<IServiceLocator>);
        }

        void ConfigureControllerFactory()
        {
            ControllerBuilder.Current.SetControllerFactory(_container.GetInstance<IControllerFactory>());
        }
    }
}