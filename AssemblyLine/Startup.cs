using AssemblyLine;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof (Startup))]

namespace AssemblyLine
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}