using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;
using AssemblyLine.DAL.Entities;
using AssemblyLine.DAL.Repositories;
using AssemblyLine.Infrastructure.Filters.Api;
using AssemblyLine.Mappings;
using AssemblyLine.Models;

namespace AssemblyLine.Controllers.Api.Dashboard
{
    [ValidateModelHttp]
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
        public async Task<IEnumerable<ProjectModel>> GetProjectStatuses(ODataQueryOptions<ProjectModel> options)
        {
            var projects = _projectsRepository.AsQueryable();
            var models =  _mapper.Project<Project, ProjectModel>(projects);
            var query = (IQueryable<ProjectModel>)options.ApplyTo(models);
            var result = await query.ToListAsync();

            // ordering milestones
            foreach (var projectModel in result)
            {
                foreach (var assemblyLine in projectModel.AssemblyLines)
                {
                    if (assemblyLine.Cycle == null) continue;

                    assemblyLine.Cycle.Milestones = assemblyLine.Cycle.Milestones.OrderBy(m => m.Position).ToList();
                }
            }

            return result;
        }

        [Route("lines/{id:int}")]
        [EnableQuery]
        public async Task<IEnumerable<ProjectCycleMilestoneModel>> GetLineStatuses(int id)
        {
            var line = await _lineRepository.GetWithMilestoneTasksAsync(id);
            if (line == null || line.Cycle == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return line.Cycle.Milestones.Select(m => _mapper.Map<ProjectCycleMilestone, ProjectCycleMilestoneModel>(m));
        }
    }
}