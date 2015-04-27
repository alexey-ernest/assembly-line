using System;
using System.Collections.Generic;

namespace AssemblyLine.DAL.Entities
{
    public class ProjectProductionCycle
    {
        public int Id { get; set; }

        public DateTime? Started { get; set; }

        public DateTime? Completed { get; set; }

        public CycleStatus Status { get; set; }

        public virtual ICollection<ProjectCycleMilestone> Milestones { get; set; }

        public virtual ProjectCycleMilestone Milestone { get; set; }

        public virtual ProjectAssemblyLine AssemblyLine { get; set; }
    }
}