using System.Web.Mvc;
using System.Web.Security;
using AssemblyLine.Configuration;
using AssemblyLine.Models;

namespace AssemblyLine.Controllers
{
    [RoutePrefix("account")]
    public class AccountController : Controller
    {
        [Route("login")]
        public ActionResult Login()
        {
            return View();
        }

        [Route("login")]
        [HttpPost]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (Membership.ValidateUser(model.UserName, model.Password))
            {
                FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                    && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                {
                    return Redirect(returnUrl);
                }

                return RedirectToRoute(RouteNames.HomeMvc);
            }

            ModelState.AddModelError(string.Empty, "The user name or password provided is incorrect.");
            return View(model);
        }

        [Route("logoff")]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToRoute(RouteNames.HomeMvc);
        }
    }
}