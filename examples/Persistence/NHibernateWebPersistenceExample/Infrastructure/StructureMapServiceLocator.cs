using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.ServiceLocation;
using StructureMap;

namespace NHibernateWebPersistenceExample.Infrastructure
{
    public class StructureMapServiceLocator : IServiceLocator
    {
        readonly IContainer _container;

        public StructureMapServiceLocator(IContainer container)
        {
            _container = container;
        }
      
        public object GetService(Type serviceType)
        {
            return _container.GetInstance(serviceType);
        }

        public object GetInstance(Type serviceType)
        {
            return _container.GetInstance(serviceType);
        }

        public object GetInstance(Type serviceType, string key)
        {
            return _container.GetInstance(serviceType, key);
        }

        public IEnumerable<object> GetAllInstances(Type serviceType)
        {
            return _container.GetAllInstances(serviceType).Cast<object>().ToList();
        }

        public TService GetInstance<TService>()
        {
            return _container.GetInstance<TService>();
        }

        public TService GetInstance<TService>(string key)
        {
            return _container.GetInstance<TService>(key);
        }

        public IEnumerable<TService> GetAllInstances<TService>()
        {
            return _container.GetAllInstances<TService>();
        }
    }
}