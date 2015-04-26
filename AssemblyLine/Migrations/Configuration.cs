using System;
using System.Data.Entity.Migrations;
using AssemblyLine.DAL;
using AssemblyLine.DAL.Entities;

namespace AssemblyLine.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            // This method will be called after migrating to the latest version.

            context.Employees.AddOrUpdate(
                e => new {e.FirstName, e.LastName},
                new Employee {FirstName = "Alexey", LastName = "Melnikov", Post = "CTO", Created = DateTime.Now.AddDays(-7)},
                new Employee { FirstName = "Brogan", LastName = "Bembrogan", Post = "Engineer", Created = DateTime.Now.AddDays(-2) },
                new Employee { FirstName = "Bob", LastName = "Dilan", Post = "Engineer", Created = DateTime.Now.AddDays(-3) },
                new Employee { FirstName = "Niko", LastName = "Williams", Post = "Engineer", Created = DateTime.Now.AddDays(-4) },
                new Employee { FirstName = "James", LastName = "Moriarty", Post = "Engineer", Created = DateTime.Now.AddDays(-2) },
                new Employee { FirstName = "Elon", LastName = "Musk", Post = "CEO", Created = DateTime.Now.AddDays(-7) }
                );

            context.Vehicles.AddOrUpdate(
                v => v.Name,
                new Vehicle {Name = "Tesla Model S 70D"},
                new Vehicle {Name = "Tesla Model S 85D"},
                new Vehicle {Name = "Tesla Model S P85D"},
                new Vehicle {Name = "Tesla Model X"}
                );
        }
    }
}