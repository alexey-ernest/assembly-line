using System.Collections.Generic;

namespace AssemblyLine.DAL.Entities
{
    public class TaskPerformer
    {
        public int Id { get; set; }

        public virtual User User { get; set; }

        public virtual MilestoneTask Task { get; set; }

        public virtual ICollection<TaskPoint> CompletedPoints { get; set; }
    }
}