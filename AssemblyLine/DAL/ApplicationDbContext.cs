using System.Data.Entity;
using AssemblyLine.DAL.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AssemblyLine.DAL
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", false)
        {
        }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Vehicle> Vehicles { get; set; }

        public DbSet<Entities.Line> Lines { get; set; }

        public DbSet<Project> Projects { get; set; }


        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}