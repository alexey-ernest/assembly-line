using System.Data.Entity.ModelConfiguration;
using AssemblyLine.DAL.Entities;

namespace AssemblyLine.DAL.Configurations
{
    public class ProjectMilestoneTaskConfiguration : EntityTypeConfiguration<ProjectMilestoneTask>
    {
        public ProjectMilestoneTaskConfiguration()
        {
            HasOptional(p => p.Performer).WithRequired(c => c.Task).WillCascadeOnDelete(false);
        }
    }
}