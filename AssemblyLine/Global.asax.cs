using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace AssemblyLine
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            var container = IoCConfig.GetConfiguredContainer();
            InitializersConfig.Register(container);
            MappingConfig.Register(container);
            JsonConfig.Configure(GlobalConfiguration.Configuration);

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}