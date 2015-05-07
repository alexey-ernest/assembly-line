using System;
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
    [ValidateModelHttp]
    [Route("api/projects/{id:int?}", Name = RouteNames.ProjectsApi)]
    public class ProjectsController : ApiController
    {
        private readonly IProjectRepository _repository;
        private readonly IMapper _mapper;

        public ProjectsController(IProjectRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [EnableQuery]
        public IQueryable<ProjectListModel> Get()
        {
            var entities = _repository.AsQueryable();
            return _mapper.Project<Project, ProjectListModel>(entities);
        }

        public async Task<Project> Get(int id)
        {
            var entity = await _repository.GetAsync(id);
            if (entity == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return entity;
        }

        public async Task<HttpResponseMessage> Post(Project model)
        {
            if (model == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.BadRequest));
            }

            model = await _repository.AddAsync(model);

            var response = Request.CreateResponse(HttpStatusCode.Created, model);
            var uri = Url.Link(RouteNames.ProjectsApi, new {id = model.Id});
            response.Headers.Location = new Uri(uri);
            return response;
        }

        public async Task<HttpResponseMessage> Put(int id, Project model)
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

        public async Task Delete(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}