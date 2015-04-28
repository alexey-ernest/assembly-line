using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.OData;
using AssemblyLine.DAL.Entities;
using AssemblyLine.DAL.Repositories;
using AssemblyLine.Infrastructure.Filters.Api;
using AssemblyLine.Mappings;
using AssemblyLine.Models;

namespace AssemblyLine.Controllers.Api.Dashboard
{
    [ValidationHttp]
    [RoutePrefix("api/dashboard")]
    public class DashboardController : ApiController
    {
        private readonly IMapper _mapper;
        private readonly IProjectRepository _projectsRepository;
        private readonly IProjectLinesRepository _lineRepository;

        public DashboardController(IProjectRepository projectsRepository, IMapper mapper, IProjectLinesRepository lineRepository)
        {
            _projectsRepository = projectsRepository;
            _mapper = mapper;
            _lineRepository = lineRepository;
        }

        [Route("projects")]
        [EnableQuery]
        public IQueryable<ProjectModel> GetProjectStatuses()
        {
            var projects = _projectsRepository.AsQueryable();
            return _mapper.Project<Project, ProjectModel>(projects);
        }

        [Route("lines/{id:int}")]
        [EnableQuery]
        public async Task<IEnumerable<ProjectLineMilestoneModel>> GetLineStatuses(int id)
        {
            var line = await _lineRepository.GetWithMilestoneTasksAsync(id);
            if (line == null || line.Cycle == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return line.Cycle.Milestones.Select(m => _mapper.Map<ProjectCycleMilestone, ProjectLineMilestoneModel>(m));
        }
    }
}