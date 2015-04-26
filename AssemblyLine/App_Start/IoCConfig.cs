using System;
using AssemblyLine.Common.Configuration;
using AssemblyLine.Common.Initializers;
using AssemblyLine.Common.Logging;
using AssemblyLine.Configuration;
using AssemblyLine.DAL;
using AssemblyLine.DAL.Repositories;
using Microsoft.Practices.Unity;

namespace AssemblyLine
{
    /// <summary>
    ///     Specifies the Unity configuration for the main container.
    /// </summary>
    public class IoCConfig
    {
        #region Unity Container

        private static readonly Lazy<IUnityContainer> Container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        ///     Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return Container.Value;
        }

        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        ///     There is no need to register concrete types such as controllers or API controllers (unless you want to
        ///     change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // Common
            container.RegisterType<IConfigurationProvider, ConfigurationProvider>(
                new ContainerControlledLifetimeManager());

            // DAL
            container.RegisterType<ApplicationDbContext, ApplicationDbContext>(new PerRequestLifetimeManager());
            container.RegisterType<IUserRepository, UserRepository>(new PerRequestLifetimeManager());
            container.RegisterType<IEmployeeRepository, EmployeeRepository>(new PerRequestLifetimeManager());
            container.RegisterType<IVehicleRepository, VehicleRepository>(new PerRequestLifetimeManager());

            // BLL


            // Services
            container.RegisterType<ILogService, TraceLogService>(new ContainerControlledLifetimeManager());

            // Initializers
            container.RegisterType<ServiceBusEmailInitializer>(new ContainerControlledLifetimeManager(),
                new InjectionFactory(
                    c =>
                        new ServiceBusEmailInitializer(
                            container.Resolve<IConfigurationProvider>().Get(SettingNames.ServiceBusConnectionString))));
            container.RegisterType<ServiceBusAuditInitializer>(new ContainerControlledLifetimeManager(),
                new InjectionFactory(
                    c =>
                        new ServiceBusAuditInitializer(
                            container.Resolve<IConfigurationProvider>().Get(SettingNames.ServiceBusConnectionString))));
            container.RegisterType<ServiceBusOutlookInitializer>(new ContainerControlledLifetimeManager(),
                new InjectionFactory(
                    c =>
                        new ServiceBusOutlookInitializer(
                            container.Resolve<IConfigurationProvider>().Get(SettingNames.ServiceBusConnectionString))));
        }
    }
}