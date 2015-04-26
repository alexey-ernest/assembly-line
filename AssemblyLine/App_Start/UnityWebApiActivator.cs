using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Filters;
using AssemblyLine;
using AssemblyLine.Infrastructure.Filters.Api;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.WebApi;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(UnityWebApiActivator), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(UnityWebApiActivator), "Shutdown")]

namespace AssemblyLine
{
    /// <summary>Provides the bootstrapping for integrating Unity with WebApi when it is hosted in ASP.NET</summary>
    public static class UnityWebApiActivator
    {
        /// <summary>Integrates Unity when the application starts.</summary>
        public static void Start() 
        {
            IUnityContainer container = IoCConfig.GetConfiguredContainer();

            // Use UnityHierarchicalDependencyResolver if you want to use a new child container for each IHttpController resolution.
            // var resolver = new UnityHierarchicalDependencyResolver(UnityConfig.GetConfiguredContainer());
            var resolver = new UnityDependencyResolver(container);

            GlobalConfiguration.Configuration.DependencyResolver = resolver;

            // Filter provider

            List<IFilterProvider> webApiFilterProviders =
                GlobalConfiguration.Configuration.Services.GetFilterProviders().ToList();

            IFilterProvider defaulWebApiFilterProvider =
                webApiFilterProviders.First(p => p is ActionDescriptorFilterProvider);
            GlobalConfiguration.Configuration.Services.Remove(typeof(IFilterProvider), defaulWebApiFilterProvider);

            GlobalConfiguration.Configuration.Services.Add(typeof(IFilterProvider),
                new UnityFilterProvider(container));
        }

        /// <summary>Disposes the Unity container when the application is shut down.</summary>
        public static void Shutdown()
        {
            var container = IoCConfig.GetConfiguredContainer();
            container.Dispose();
        }
    }
}
