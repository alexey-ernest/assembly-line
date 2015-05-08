using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using AssemblyLine.DAL.Entities;
using AssemblyLine.Mappings;

namespace AssemblyLine.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMapper _mapper;

        public UserRepository(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IQueryable<User> AsQueryable()
        {
            var membershipUsers = new MembershipUser[] {};
            Membership.GetAllUsers().CopyTo(membershipUsers, 0);

            IEnumerable<User> users = membershipUsers.Select(u => _mapper.Map<MembershipUser, User>(u));
            return users.AsQueryable();
        }

        public User Get(string id)
        {
            return AsQueryable().FirstOrDefault(u => u.Id == id);
        }

        public IQueryable<User> GetUsersInRole(string roleName)
        {
            string[] userIds = Roles.GetUsersInRole(roleName);
            IQueryable<User> users = AsQueryable().Where(u => userIds.Contains(u.Id));
            return users;
        }

        public string[] GetUserRoles(string id)
        {
            string[] roles = Roles.GetRolesForUser(id);
            return roles;
        }
    }
}