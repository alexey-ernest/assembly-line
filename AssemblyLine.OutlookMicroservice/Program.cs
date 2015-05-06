using System;
using System.DirectoryServices.Protocols;
using System.Net;

namespace AssemblyLine.OutlookMicroservice
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine(LdapTest());
            Console.ReadKey();
        }

        private static bool LdapTest()
        {
            const string server = "ldap.forumsys.com:389";
            const string userName = "uid=tesla,dc=example,dc=com";
            const string password = "password";

            try
            {
                using (var connection = new LdapConnection(server))
                {
                    connection.Timeout = new TimeSpan(0, 0, 10);
                    connection.AuthType = AuthType.Basic;
                    connection.SessionOptions.ProtocolVersion = 3; // Set protocol to LDAPv3

                    var credential = new NetworkCredential(userName, password);
                    connection.Bind(credential);
                }
                
                // If the bind succeeds, the credentials are valid
                return true;
            }
            catch (LdapException ldapEx)
            {
                
                // The supplied credential is invalid.
                if (ldapEx.ErrorCode.Equals(49))
                {
                    return false;
                }

                throw;
            }
        }
    }
}