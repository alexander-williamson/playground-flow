using System.Data;
using System.Data.SqlClient;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Flow.Library.Configuration;
using Flow.Library.Data.Abstract;
using Flow.Web.Configuration;

namespace Flow.Web
{
    public class WindsorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromThisAssembly()
                .BasedOn<IController>()
                .LifestyleTransient());

            container.Register(Classes.FromThisAssembly().BasedOn<IHttpController>().LifestyleTransient());

            container.Register(
                Component.For<IConfigurationProvider>()
                    .ImplementedBy<ConfigurationManagerProvider>()
                    .LifestyleSingleton());

            container.Register(Component.For<IConfiguration>()
                .ImplementedBy<WebConfiguration>().LifestyleSingleton());

            container.Register(Component.For<IDbConnection>()
                .UsingFactoryMethod(() => GetConnection(container.Resolve<IConfiguration>())).LifestyleTransient());

            container.Register(Component.For<IUnitOfWork>()
                .ImplementedBy<SqlUnitOfWork>());
        }

        private static IDbConnection GetConnection(IConfiguration configuration)
        {
            var connection = new SqlConnection(configuration.ConnectionString);
            connection.Open();
            return connection;
        }

    }
}