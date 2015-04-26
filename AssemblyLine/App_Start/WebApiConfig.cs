using System.Web.Http;
using AssemblyLine.Infrastructure.Filters;
using AssemblyLine.Infrastructure.Filters.Api;
using Microsoft.Owin.Security.OAuth;

namespace AssemblyLine
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            // Exception logging in api controllers
            config.Filters.Add((HttpExceptionHandlingAttribute)config.DependencyResolver.GetService(typeof(HttpExceptionHandlingAttribute)));
        }
    }
}