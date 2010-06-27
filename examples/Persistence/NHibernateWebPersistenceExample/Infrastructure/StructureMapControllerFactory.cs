using System.Web.Mvc;
using System.Web.Routing;
using StructureMap;

namespace NHibernateWebPersistenceExample.Infrastructure
{
    public class StructureMapControllerFactory : DefaultControllerFactory
    {
        readonly IContainer _container;

        public StructureMapControllerFactory(IContainer container)
        {
            _container = container;
        }

        public override IController CreateController(RequestContext requestContext, string controllerName)
        {
            return _container.GetInstance<IController>(controllerName);
        }
    }
}