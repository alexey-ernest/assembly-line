using System;
using System.Linq;
using System.Web.Http.Controllers;
using AssemblyLine.Infrastructure.Authorization;

namespace AssemblyLine.Infrastructure.Attributes.Http
{
    public class AuthorizeActiveDirectoryHttpAttribute : AuthorizeHttpAttribute
    {
        public string Groups { get; set; }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            if (!base.IsAuthorized(actionContext))
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
                return LdapAuthorizationHelper.UserIsMemberOfGroups(
                    actionContext.RequestContext.Principal.Identity.Name, groups);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}