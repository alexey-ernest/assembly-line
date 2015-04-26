using System.Web.Mvc;
using AssemblyLine.Configuration;

namespace AssemblyLine.Controllers
{
    [RoutePrefix("")]
    public class HomeController : Controller
    {
        [Route("employees/{id?}")]
        [Route("vehicles/{id?}")]
        [Route("projects/{id?}")]
        [Route(Name = RouteNames.HomeMvc)]
        public ActionResult Index()
        {
            return View();
        }
    }
}