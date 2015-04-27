using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AssemblyLine.BLL;
using AssemblyLine.Configuration;
using AssemblyLine.Infrastructure.Filters.Api;

namespace AssemblyLine.Controllers.Api.Projects
{
    [ValidationHttp]
    [Route("api/projects/{id:int}/cycle", Name = RouteNames.ProjectCycleApi)]
    public class ProjectCycleController : ApiController
    {
        private readonly IProjectService _service;

        public ProjectCycleController(IProjectService service)
        {
            _service = service;
        }

        public async Task<HttpResponseMessage> Post(int id)
        {
            await _service.KickOffProject(id);

            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }
    }
}