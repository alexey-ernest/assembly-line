using System.Data.Entity.ModelConfiguration;
using AssemblyLine.DAL.Entities;

namespace AssemblyLine.DAL.Configurations
{
    public class ProjectAssemblyLineConfiguration : EntityTypeConfiguration<ProjectAssemblyLine>
    {
        public ProjectAssemblyLineConfiguration()
        {
            HasOptional(p => p.Cycle).WithRequired(c => c.AssemblyLine).WillCascadeOnDelete(false);
        }
    }
}