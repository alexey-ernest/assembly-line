using System.Data.Entity.ModelConfiguration;
using AssemblyLine.DAL.Entities;

namespace AssemblyLine.DAL.Configurations
{
    public class ProjectConfiguration : EntityTypeConfiguration<Project>
    {
        public ProjectConfiguration()
        {
            HasOptional(p => p.Cycle).WithRequired(c => c.Project).WillCascadeOnDelete(false);
        }
    }
}