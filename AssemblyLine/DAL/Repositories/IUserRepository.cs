using System.Linq;
using System.Threading.Tasks;
using AssemblyLine.DAL.Entities;

namespace AssemblyLine.DAL.Repositories
{
    public interface IUserRepository : IRepository
    {
        IQueryable<User> AsQueryable();

        Task<User> GetAsync(string id);

        IQueryable<User> GetUsersInRole(string roleName);

        Task<string[]> GetUserRolesAsync(string id);
    }
}