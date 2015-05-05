using System.Data.Entity.ModelConfiguration;
using AssemblyLine.DAL.Entities;

namespace AssemblyLine.DAL.Configurations
{
    public class ProjectAssemblyLineConfiguration : EntityTypeConfiguration<ProjectLine>
    {
        public ProjectAssemblyLineConfiguration()
        {
            HasOptional(p => p.Cycle).WithRequired(c => c.Line).WillCascadeOnDelete(false);
        }
    }
}