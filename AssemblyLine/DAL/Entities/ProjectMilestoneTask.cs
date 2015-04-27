using System;

namespace AssemblyLine.DAL.Entities
{
    public class ProjectMilestoneTask
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime? Started { get; set; }

        public DateTime? Completed { get; set; }

        public TaskStatus Status { get; set; }

        public virtual TaskPerformer Performer { get; set; }

        public virtual ProjectCycleMilestone Milestone { get; set; }
    }
}