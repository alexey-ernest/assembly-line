using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AssemblyLine.Infrastructure.Authorization;

namespace AssemblyLine.Infrastructure.Attributes.Mvc
{
    public class AuthorizeActiveDirectoryAttribute : AuthorizeAttribute
    {
        public string Groups { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (!base.AuthorizeCore(httpContext))
            {
                return false;
            }

            if (string.IsNullOrEmpty(Groups))
            {
                return true;
            }

            string[] groups = Groups.Split(',').Select(g => g.Trim()).ToArray();

            try
            {
                return LdapAuthorizationHelper.UserIsMemberOfGroups(httpContext.User.Identity.Name, groups);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}