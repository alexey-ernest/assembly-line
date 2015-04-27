using System.Collections.Generic;

namespace AssemblyLine.DAL.Entities
{
    public class TaskPerformer
    {
        public int Id { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual ProjectMilestoneTask Task { get; set; }

        public virtual ICollection<TaskPoint> CheckPoints { get; set; }

        public virtual ICollection<TaskPoint> CompletedPoints { get; set; }
    }
}