using System.Linq;
using AssemblyLine.DAL.Entities;

namespace AssemblyLine.DAL.Repositories
{
    public interface IUserRepository
    {
        IQueryable<User> AsQueryable();

        User Get(string id);

        IQueryable<User> GetUsersInRole(string roleName);

        string[] GetUserRoles(string id);
    }
}