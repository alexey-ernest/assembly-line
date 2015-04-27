using System.Linq;
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
        private readonly IProjectRepository _repository;

        public DashboardController(IProjectRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [Route("projects")]
        [EnableQuery]
        public IQueryable<ProjectModel> GetProjectStatuses()
        {
            var entities = _repository.AsQueryable();
            return _mapper.Project<Project, ProjectModel>(entities);
        }
    }
}