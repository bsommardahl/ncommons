using System;
using System.Web;
using NHibernateWebPersistenceExample.Infrastructure;

namespace NHibernateWebPersistenceExample
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            new Bootstrapper().Run();
        }

        protected void Application_BeginRequest()
        {
            DatabaseContextFactory.GetDatabaseContext().OpenSession();
        }

        protected void Application_EndRequest()
        {
            var databaseContext = DatabaseContextFactory.GetDatabaseContext();

            try
            {
                databaseContext.CommitSession();
            }
            catch (Exception)
            {
                databaseContext.GetCurrentSession().Rollback();
            }
            finally
            {
                databaseContext.GetCurrentSession().Dispose();
            }
        }
    }
}