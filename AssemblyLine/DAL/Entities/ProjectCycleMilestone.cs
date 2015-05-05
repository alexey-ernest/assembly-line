using System;
using System.Collections.Generic;

namespace AssemblyLine.DAL.Entities
{
    public class ProjectCycleMilestone
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Position { get; set; }

        public DateTime? Started { get; set; }

        public DateTime? Completed { get; set; }

        public MilestoneStatus Status { get; set; }

        public virtual ProjectLineCycle Cycle { get; set; }

        public virtual ICollection<ProjectMilestoneTask> Tasks { get; set; }
    }
}