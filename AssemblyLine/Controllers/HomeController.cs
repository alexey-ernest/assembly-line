﻿using System.Web.Mvc;
using AssemblyLine.Configuration;
using AssemblyLine.Constants;
using AssemblyLine.Properties;

namespace AssemblyLine.Controllers
{
    [Authorize(Roles = UserRoles.Engineers)]
    [RoutePrefix("")]
    public class HomeController : Controller
    {
        [Route("employees/{*url}")]
        [Route("vehicles/{*url}")]
        [Route("projects/{*url}")]
        [Route("lines/{*url}")]
        [Route("dashboard/{*url}")]
        [Route(Name = RouteNames.HomeMvc)]
        public ActionResult Index()
        {
            ViewBag.Title = Settings.Default.ApplicationName;

            return View();
        }
    }
}