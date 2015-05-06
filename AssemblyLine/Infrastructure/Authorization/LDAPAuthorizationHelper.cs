using System.DirectoryServices.AccountManagement;
using System.Linq;

namespace AssemblyLine.Infrastructure.Authorization
{
    public class LdapAuthorizationHelper
    {
        public static bool UserIsMemberOfGroups(string username, string[] groups)
        {
            if (groups == null || groups.Length == 0)
            {
                return true;
            }

            // Verify that the user is in the given AD group (if any)
            using (var context = new PrincipalContext(ContextType.Domain))
            {
                UserPrincipal userPrincipal = UserPrincipal.FindByIdentity(context, IdentityType.SamAccountName,
                    username);
                if (userPrincipal == null)
                {
                    return false;
                }

                if (groups.Any(@group => userPrincipal.IsMemberOf(context, IdentityType.Name, @group)))
                {
                    return true;
                }
            }

            return false;
        }
    }
}