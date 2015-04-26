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

namespace AssemblyLine.Controllers.Api
{
    [ValidationHttp]
    [Route("api/employees/{id:int?}", Name = RouteNames.EmployeeApi)]
    public class EmployeesController : ApiController
    {
        private readonly IEmployeeRepository _repository;

        public EmployeesController(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        // GET api/employees

        [EnableQuery]
        public IQueryable<Employee> Get()
        {
            IQueryable<Employee> entities = _repository.AsQueryable();
            return entities;
        }


        // GET api/employees/{id}

        public async Task<Employee> Get(int id)
        {
            var entity = await _repository.GetAsync(id);
            if (entity == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return entity;
        }


        // POST api/employees

        public async Task<HttpResponseMessage> Post(Employee model)
        {
            if (model == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.BadRequest));
            }

            model = await _repository.AddAsync(model);

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, model);
            string uri = Url.Link(RouteNames.EmployeeApi, new { id = model.Id });
            response.Headers.Location = new Uri(uri);
            return response;
        }


        // PUT api/admin/articles/{id}

        public async Task<HttpResponseMessage> Put(int id, Employee model)
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


        // DELETE api/admin/articles/{id}

        public async Task Delete(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}