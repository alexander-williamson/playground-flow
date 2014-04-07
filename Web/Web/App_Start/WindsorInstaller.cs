using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Flow.Library.Data.Abstract;

namespace Flow.Web
{
    public class WindsorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromThisAssembly()
                .BasedOn<IController>()
                .LifestyleTransient());

            container.Register(Component.For<IDbConnection>()
                .ImplementedBy<SqlConnection>());

            container.Register(Component.For<IUnitOfWork>()
                .ImplementedBy<SqlUnitOfWork>());
        }
    }
}