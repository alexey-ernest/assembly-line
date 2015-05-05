using System.Data.Entity;
using AssemblyLine.DAL.Configurations;
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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ProjectAssemblyLineConfiguration());
            modelBuilder.Configurations.Add(new ProjectMilestoneTaskConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Vehicle> Vehicles { get; set; }

        public DbSet<Line> Lines { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<AssemblyLineTeam> AssemblyLineTeams { get; set; }

        public DbSet<ProjectLine> ProjectLines { get; set; }

        public DbSet<ProductionCycle> ProductionCycles { get; set; }

        public DbSet<CycleMilestone> CycleMilestones { get; set; }

        public DbSet<MilestoneTask> MilestoneTasks { get; set; }

        public DbSet<TaskPoint> TaskPoints { get; set; }


        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}