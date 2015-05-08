using System.Threading.Tasks;
using System.Web.Mvc;
using AssemblyLine.Configuration;
using AssemblyLine.Constants;
using AssemblyLine.DAL.Repositories;
using AssemblyLine.Models;
using AssemblyLine.Properties;

namespace AssemblyLine.Controllers
{
    [Authorize(Roles = UserRoles.Engineers)]
    [RoutePrefix("")]
    public class HomeController : Controller
    {
        private readonly IUserRepository _userRepository;

        public HomeController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [Route("employees/{*url}")]
        [Route("vehicles/{*url}")]
        [Route("projects/{*url}")]
        [Route("lines/{*url}")]
        [Route("dashboard/{*url}")]
        [Route(Name = RouteNames.HomeMvc)]
        public ActionResult Index()
        {
            ViewBag.Title = Settings.Default.ApplicationName;

            var vm = new HomeViewModel();
            if (Request.IsAuthenticated)
            {
                var userRoles = _userRepository.GetUserRoles(User.Identity.Name);
                vm.UserRoles = userRoles;
            }
            else
            {
                vm.UserRoles = new string[] {};
            }

            return View(vm);
        }
    }
}