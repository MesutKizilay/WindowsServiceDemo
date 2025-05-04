using Autofac;
using System.ComponentModel;
using System.ServiceProcess;
using WindowsServiceDemo.DataAccess;
using WindowsServiceDemo.Helper;

namespace WindowsServiceDemo
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            try
            {
                //ServiceBase[] ServicesToRun;
                //ServicesToRun = new ServiceBase[]
                //{
                //    new Service1(new CarDal(new TsContext()))
                //};
                //ServiceBase.Run(ServicesToRun);

                // New DI Container
                ContainerBuilder containerBuilder = new ContainerBuilder();

                // Load types into the container

                // The Windows service itself
                containerBuilder.RegisterType<Service1>().AsSelf().InstancePerLifetimeScope();

                // Repositories
                //containerBuilder.RegisterType<ForecastRepository>().As<IForecastRepository>().InstancePerLifetimeScope();

                // Logging
                //containerBuilder.RegisterType<Log>().As<ILog>().InstancePerLifetimeScope();

                // Service classes
                containerBuilder.RegisterType<CarDal>().As<ICarDal>().InstancePerLifetimeScope();
                containerBuilder.RegisterType<TsContext>().InstancePerLifetimeScope();

                // Finalise the container
                Autofac.IContainer container = containerBuilder.Build();

                // Ask the service to be run from the Dependency Injection container.
                // By doing this all the other interfaces/implementations will be available
                // through constructor matching
                ServiceBase.Run(container.Resolve<Service1>());
            }
            catch (System.Exception ex)
            {
                LogHelper.LogError(ex);
            }
        }
    }
}
