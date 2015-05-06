using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace AssemblyLine.Infrastructure.Attributes.Http
{
    public class AuthorizeHttpAttribute : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            IPrincipal user = actionContext.ControllerContext.RequestContext.Principal;
            if (user == null || !user.Identity.IsAuthenticated)
            {
                actionContext.Response = actionContext.ControllerContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            else
            {
                actionContext.Response = actionContext.ControllerContext.Request.CreateResponse(HttpStatusCode.Forbidden);
            }
        }
    }
}