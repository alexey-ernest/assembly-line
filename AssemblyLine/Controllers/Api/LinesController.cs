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
    [Route("api/lines/{id:int?}", Name = RouteNames.LinesApi)]
    public class LinesController : ApiController
    {
        private readonly ILineRepository _repository;

        public LinesController(ILineRepository repository)
        {
            _repository = repository;
        }

        [EnableQuery]
        public IQueryable<Line> Get()
        {
            IQueryable<Line> entities = _repository.AsQueryable();
            return entities;
        }

        public async Task<Line> Get(int id)
        {
            var entity = await _repository.GetAsync(id);
            if (entity == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return entity;
        }

        public async Task<HttpResponseMessage> Post(Line model)
        {
            if (model == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.BadRequest));
            }

            model = await _repository.AddAsync(model);

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, model);
            string uri = Url.Link(RouteNames.VehiclesApi, new { id = model.Id });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        public async Task<HttpResponseMessage> Put(int id, Line model)
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