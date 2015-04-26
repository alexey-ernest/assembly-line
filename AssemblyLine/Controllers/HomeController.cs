using System.Web.Mvc;
using AssemblyLine.Configuration;

namespace AssemblyLine.Controllers
{
    [RoutePrefix("")]
    public class HomeController : Controller
    {
        [Route("employees/{*url}")]
        [Route("vehicles/{*url}")]
        [Route("projects/{*url}")]
        [Route("lines/{*url}")]
        [Route(Name = RouteNames.HomeMvc)]
        public ActionResult Index()
        {
            return View();
        }
    }
}