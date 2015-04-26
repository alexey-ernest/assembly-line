using System.Web.Mvc;
using AssemblyLine.Configuration;

namespace AssemblyLine.Controllers
{
    public class HomeController : Controller
    {
        [Route("/", Name = RouteNames.HomeMvc)]
        public ActionResult Index()
        {
            return View();
        }
    }
}