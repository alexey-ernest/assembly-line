using System;
using System.Data.Entity;
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

namespace AssemblyLine.Controllers.Api.Projects
{
    [ValidationHttp]
    [Route("api/projects/{id:int?}", Name = RouteNames.ProjectsApi)]
    public class ProjectsController : ApiController
    {
        private readonly IProjectRepository _repository;

        public ProjectsController(IProjectRepository repository)
        {
            _repository = repository;
        }

        [EnableQuery]
        public IQueryable<Project> Get()
        {
            IQueryable<Project> entities = _repository.AsQueryable().Include(p => p.Vehicle);
            return entities;
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

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, model);
            string uri = Url.Link(RouteNames.ProjectsApi, new { id = model.Id });
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

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, model);
            return response;
        }

        public async Task Delete(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}