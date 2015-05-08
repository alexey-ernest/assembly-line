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
    [ValidateModelHttp]
    [Route("api/users/{id?}", Name = RouteNames.UsersApi)]
    public class UsersController : ApiController
    {
        private readonly IUserRepository _repository;

        public UsersController(IUserRepository repository)
        {
            _repository = repository;
        }

        [EnableQuery]
        public IQueryable<User> Get()
        {
            IQueryable<User> entities = _repository.AsQueryable();
            return entities;
        }

        public User Get(string id)
        {
            User entity = _repository.Get(id);
            if (entity == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return entity;
        }
    }
}