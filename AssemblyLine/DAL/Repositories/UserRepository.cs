using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AssemblyLine.DAL.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AssemblyLine.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;

        public UserRepository(ApplicationDbContext db)
        {
            _db = db;
        }


        public void Dispose()
        {
            _db.Dispose();
        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }

        public IQueryable<User> AsQueryable()
        {
            return _db.Users;
        }

        public IQueryable<User> GetUsersInRole(string roleName)
        {
            IdentityRole role = _db.Roles.FirstOrDefault(r => r.Name == roleName);
            if (role == null)
            {
                return new List<User>().AsQueryable();
            }

            return _db.Users.Where(u => u.Roles.Any(r => r.RoleId == role.Id));
        }

        public async Task<string[]> GetUserRolesAsync(string id)
        {
            string[] userRoleIds =
                await _db.Users.Where(u => u.Id == id).SelectMany(u => u.Roles.Select(ur => ur.RoleId)).ToArrayAsync();
            return await _db.Roles.Where(r => userRoleIds.Contains(r.Id)).Select(r => r.Name).ToArrayAsync();
        }
    }
}