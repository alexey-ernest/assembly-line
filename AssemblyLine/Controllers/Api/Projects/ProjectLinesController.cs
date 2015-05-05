using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.OData;
using AssemblyLine.Configuration;
using AssemblyLine.DAL.Entities;
using AssemblyLine.DAL.Repositories;
using AssemblyLine.Infrastructure.Filters.Api;
using AssemblyLine.Mappings;
using AssemblyLine.Models;

namespace AssemblyLine.Controllers.Api.Projects
{
    [ValidationHttp]
    [Route("api/projects/{pid:int}/lines/{id:int?}", Name = RouteNames.ProjectLinesApi)]
    public class ProjectLinesController : ApiController
    {
        private readonly IProjectLinesRepository _repository;
        private readonly IMapper _mapper;

        public ProjectLinesController(IProjectLinesRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [EnableQuery]
        public async Task<IEnumerable<ProjectLineListModel>> Get(int pid)
        {
            var entities = await _repository.GetByProjectAsync(pid);
            return entities.Select(l => _mapper.Map<ProjectLine, ProjectLineListModel>(l)).ToList();
        }

        public async Task<ProjectLineModel> Get(int pid, int id)
        {
            var entity = await _repository.GetAsync(id);
            if (entity == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return _mapper.Map<ProjectLine, ProjectLineModel>(entity);
        }

        public async Task<HttpResponseMessage> Put(int pid, int id, ProjectLine model)
        {
            if (model == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.BadRequest));
            }

            model.Id = id;
            model = await _repository.EditAsync(model);

            var response = Request.CreateResponse(HttpStatusCode.OK, model);
            return response;
        }
    }
}