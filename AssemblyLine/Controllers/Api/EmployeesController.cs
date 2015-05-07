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

namespace AssemblyLine.Controllers.Api
{
    [ValidateModelHttp]
    [Route("api/employees/{id:int?}", Name = RouteNames.EmployeeApi)]
    public class EmployeesController : ApiController
    {
        private readonly IEmployeeRepository _repository;
        private readonly IMapper _mapper;

        public EmployeesController(IEmployeeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [EnableQuery]
        public IQueryable<EmployeeListModel> Get()
        {
            var entities = _repository.AsQueryable();
            return _mapper.Project<Employee, EmployeeListModel>(entities);
        }

        public async Task<Employee> Get(int id)
        {
            var entity = await _repository.GetAsync(id);
            if (entity == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return entity;
        }

        public async Task<HttpResponseMessage> Post(Employee model)
        {
            if (model == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.BadRequest));
            }

            model = await _repository.AddAsync(model);

            var response = Request.CreateResponse(HttpStatusCode.Created, model);
            var uri = Url.Link(RouteNames.EmployeeApi, new {id = model.Id});
            response.Headers.Location = new Uri(uri);
            return response;
        }

        public async Task<HttpResponseMessage> Put(int id, Employee model)
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