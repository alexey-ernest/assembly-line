using System;
using System.Collections.Generic;

namespace AssemblyLine.DAL.Entities
{
    public class CycleMilestone
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Position { get; set; }

        public DateTime? Started { get; set; }

        public DateTime? Completed { get; set; }

        public MilestoneStatus Status { get; set; }

        public virtual ICollection<MilestoneTask> Tasks { get; set; }
    }
}